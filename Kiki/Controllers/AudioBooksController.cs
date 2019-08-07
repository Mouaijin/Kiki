using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.FileSys;
using Kiki.Identification;
using Kiki.Models;
using Kiki.Models.Data;
using Kiki.Models.Scanning;
using Kiki.Models.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kiki.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AudioBooksController : ControllerBase
    {
        private readonly KikiContext _context;

        public AudioBooksController(KikiContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<AudioBook>> List([FromQuery] int count = 25, [FromQuery] int offset = 0)
        {
            List<AudioBook> list = await _context.AudioBooks.Skip(offset).Take(count).ToListAsync();
            return list;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AudioBook>> Get([FromQuery] Guid id)
        {
            AudioBook audioBook = await _context.AudioBooks.SingleOrDefaultAsync(x => x.Id == id);
            return audioBook;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AudioFile>>> Files([FromQuery] Guid id)
        {
            AudioBook audioBook = await _context.AudioBooks.SingleOrDefaultAsync(x => x.Id == id);
            if (audioBook == null)
            {
                return NotFound();
            }

            return audioBook.Files;
        }

        [Authorize]
        [HttpGet]
        public async Task<AudioBookProgress> Progress([FromQuery] Guid id)
        {
            AudioBookProgress progress =
                await _context.BookProgresses.SingleOrDefaultAsync(x => x.Id == id && x.UserId == User.GetUserId());

            return progress;
        }
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AudioFileController : ControllerBase
    {
        private readonly KikiContext _context;

        public AudioFileController(KikiContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<AudioFile> Get([FromQuery] Guid id)
        {
            AudioFile file = await _context.AudioFiles.SingleOrDefaultAsync(x => x.Id == id);
            return file;
        }

        [Authorize]
        [HttpGet]
        public async Task<AudioFileProgress> Progress([FromQuery] Guid id)
        {
            AudioFileProgress progress = await _context.FileProgresses.SingleOrDefaultAsync(x => x.Id == id && x.UserId == User.GetUserId());
            return progress;
        }
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScanController : ControllerBase
    {
        private readonly KikiContext _context;

        public ScanController(KikiContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet]
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