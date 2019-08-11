using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Data;
using Kiki.Models.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kiki.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AudioBooksController : ControllerBase
    {
        private readonly KikiContext                   _context;
        private readonly ILogger<AudioBooksController> _logger;

        public AudioBooksController(KikiContext dbContext, ILogger<AudioBooksController> logger)
        {
            _context = dbContext;
            _logger  = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AudioBook>>> List([FromQuery] int count = 25, [FromQuery] int offset = 0)
        {
            try
            {
                List<AudioBook> list = await _context.AudioBooks.Skip(offset).Take(count).ToListAsync();
                if (list == null)
                {
                    throw new ArgumentNullException(); //Skip to exception in case of impossible null
                }

                _logger.LogInformation($"Books listed: {list.Count}");
                return list;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Tried to list audiobooks, but received null list. Count {count}, offset {offset}");
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AudioBook>> Get([FromQuery] Guid id)
        {
            AudioBook audioBook = await _context.AudioBooks.SingleOrDefaultAsync(x => x.Id == id);
            if (audioBook == null)
            {
                _logger.LogError($"Could not retrieve audiobook with ID {id.ToString()}");
                return NotFound();
            }

            _logger.LogInformation($"Book {id.ToString()} retrieved");

            return audioBook;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AudioFile>>> Files([FromQuery] Guid id)
        {
            AudioBook audioBook = await _context.AudioBooks.SingleOrDefaultAsync(x => x.Id == id);
            if (audioBook == null)
            {
                _logger.LogError($"Could not retrieve files for audiobook with ID {id.ToString()}");

                return NotFound();
            }

            _logger.LogInformation($"Files retrieved for book {id.ToString()}");

            return audioBook.Files;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AudioBookProgress>> Progress([FromQuery] Guid id)
        {
            AudioBookProgress progress =
                await _context.BookProgresses.SingleOrDefaultAsync(x => x.Id == id && x.UserId == User.GetUserId());
            if (progress == null)
            {
                _logger.LogError($"Could not retrieve progress for audiobook with ID {id.ToString()}");

                return NotFound();
            }

            _logger.LogInformation($"Progress retrieved for audiobook {id.ToString()}");
            return progress;
        }
    }
}