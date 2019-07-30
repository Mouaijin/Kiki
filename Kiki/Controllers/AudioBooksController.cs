using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<AudioBook> Get([FromQuery] Guid id)
        {
            AudioBook audioBook = await _context.AudioBooks.SingleOrDefaultAsync(x => x.Id == id);
            return audioBook;
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
}