using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.FileSys;
using Kiki.Identification;
using Kiki.Models;
using Kiki.Models.Data;
using Kiki.Models.Scanning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Kiki.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScanController : ControllerBase
    {
        private readonly KikiContext             _context;
        private readonly ILogger<ScanController> _logger;

        public ScanController(KikiContext dbContext, ILogger<ScanController> logger)
        {
            _context = dbContext;
            _logger  = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("api/scan/update")]
        public async Task<ActionResult> Update()
        {
            DirectoryScanner directoryScanner = new DirectoryScanner();
            BookInfoScanner  infoScanner      = new BookInfoScanner();
            foreach (MediaDirectory dir in _context.MediaDirectories.ToList())
            {
                List<AudioBook> scannedBooks = directoryScanner.ScanForBooks(dir.DirectoryPath);
                foreach (AudioBook scannedBook in scannedBooks)
                {
                    if (_context.AudioBooks.Any(x => x.AudioBookDirectoryPath == scannedBook.AudioBookDirectoryPath))
                    {
                        continue;
                    }
                    //todo: info scanning

                    EntityEntry<AudioBook> bookEntry      = _context.AudioBooks.Add(scannedBook);
                    var                    entitesCreated = await _context.SaveChangesAsync();
                    //todo: log entries created
                }
            }

            return Ok();
        }
    }
}