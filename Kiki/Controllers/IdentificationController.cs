using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Google;
using Kiki.Identification;
using Kiki.Models;
using Kiki.Models.Data;
using Kiki.Models.Metadata;
using Kiki.Models.Requests.Identification;
using Kiki.Models.Scanning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kiki.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IdentificationController : ControllerBase
    {
        private readonly KikiContext                       _context;
        private readonly ILogger<IdentificationController> _logger;

        public IdentificationController(KikiContext dbContext, ILogger<IdentificationController> logger)
        {
            _context = dbContext;
            _logger  = logger;
        }

        /// <summary>
        /// Gets possible matches from Google Books API
        /// </summary>
        /// <param name="type">"title" or "isbn" for specific searches, blank or anything else for a general search</param>
        /// <param name="query">string to search</param>
        /// <returns>List of possible matches from Google Books API</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<GoogleBook>>> Search([FromQuery] string type  = "",
                                                                 [FromQuery] string query = "")
        {
            try
            {
                BookInfoScanner scanner = new BookInfoScanner();
                switch (type)
                {
                    case "title":
                        return Ok(await scanner.SearchByTitleAsync(query));
                    case "isbn":
                        return
                            Ok(await scanner.SearchBookByISBNAsync(query));
                    default:
                        return Ok(await scanner.SearchGeneralAsync(query));
                }
            }
            catch (Exception ex)
            {
                _logger
                    .LogError($"Error fetching books query: ({type}){query}: {ex.Message}");
                return StatusCode((int) HttpStatusCode
                                      .InternalServerError);
            }
        }

        /// <summary>
        /// Updates Kiki audiobook information with information from given Google Books API ID
        /// </summary>
        /// <param name="model">Kiki Audiobook ID and Google Books Volume ID</param>
        /// <returns>Http status code</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Confirm([FromBody] BookInfoConfirmationModel model)
        {
            try
            {
                BookInfoScanner scanner = new BookInfoScanner();
                var             gBook   = await scanner.GetGoogleBookAsync(model.GoogleBooksId);
                if (gBook == null)
                {
                    return BadRequest($"Google volume '{model.GoogleBooksId}' not found");
                }

                AudioBook kBook = await _context.AudioBooks.SingleOrDefaultAsync(x => x.Id == model.KikiBookId);
                if (kBook == null)
                {
                    return BadRequest($"Kiki audiobook with ID '{model.KikiBookId.ToString()}' not found");

                }

                kBook.Title = gBook.Title;
                foreach (string newAuthor in gBook.Authors.Where(x => kBook.Authors.All(a => a.Author.Name != x)))
                {
                    if (_context.Authors.All(x => x.Name != newAuthor))
                    {
                        kBook.Authors.Add(new BookAuthor(kBook, new Author(newAuthor)));
                    }
                    else
                    {
                        var author = await _context.Authors.SingleOrDefaultAsync(x => x.Name == newAuthor);
                        kBook.Authors.Add(new BookAuthor(kBook, author));
                    }
                }

                kBook.Description = gBook.Description;

                if (!string.IsNullOrEmpty(gBook.ThumbnailLink))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var thumbnailBytes = await client.GetByteArrayAsync(gBook.ThumbnailLink);
                        kBook.ThumbnailData = Convert.ToBase64String(thumbnailBytes);
                    }
                }

                kBook.Language = gBook.Language;
                if (!string.IsNullOrEmpty(gBook.Publisher))
                {
                    if (_context.Publishers.All(x => x.Name != gBook.Publisher))
                    {
                        kBook.Publisher = new Publisher() {Name = gBook.Publisher};
                    }
                    else
                    {
                        kBook.Publisher =
                            await _context.Publishers.SingleOrDefaultAsync(x => x.Name == gBook.Publisher);
                    }
                }

                if (gBook.Published.HasValue)
                {
                    kBook.Year = gBook.Published.Value.Year;
                }

                kBook.GoogleBooksID = gBook.GoogleBooksID;

                foreach (var identifier in gBook.IndustryIdentifiers)
                {
                    if (identifier.IdentificationType == "ISBN10")
                    {
                        kBook.ISBN10 = identifier.IdentificationCode;
                    }
                    else if (identifier.IdentificationType == "ISBN13")
                    {
                        kBook.ISBN13 = identifier.IdentificationCode;
                    }
                }

                var changed = await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is GoogleApiException gae)
                {
                    string err = $"Google API error: {gae.Error.Message}";
                    _logger.LogError(err);
                    return BadRequest(err);
                }
                if (ex is DbUpdateException due)
                {
                    _logger.LogError($"Error saving changes to book with ID {model.KikiBookId} from Google book {model.GoogleBooksId}: {due.Message}");
                }
                else
                {
                    _logger.LogError($"Unexpected error while updating book: {ex.Message}");
                }

                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}