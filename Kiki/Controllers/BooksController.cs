using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kiki.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly KikiContext _context;

        public BooksController(KikiContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Book>> List([FromQuery] int count = 25, [FromQuery] int offset = 0)
        {
            List<Book> list = await _context.Books.Skip(offset).Take(count).ToListAsync();
            return list;
        }
        
    }
}