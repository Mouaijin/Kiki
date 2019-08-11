using System;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kiki.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AudioFileController : ControllerBase
    {
        private readonly KikiContext                  _context;
        private readonly ILogger<AudioFileController> _logger;

        public AudioFileController(KikiContext dbContext, ILogger<AudioFileController> logger)
        {
            _context = dbContext;
            _logger  = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AudioFile>> Get([FromQuery] Guid id)
        {
            AudioFile file = await _context.AudioFiles.SingleOrDefaultAsync(x => x.Id == id);
            if (file == null)
            {
                _logger.LogError($"Could not retrieve progress for audio file with ID {id.ToString()}");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved audio file with ID {id.ToString()}");
            return file;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AudioFileProgress>> Progress([FromQuery] Guid id)
        {
            AudioFileProgress progress =
                await _context.FileProgresses.SingleOrDefaultAsync(x => x.Id == id && x.UserId == User.GetUserId());
            if (progress == null)
            {
                _logger.LogError($"Could not retrieve progress for audio file with ID {id.ToString()}");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved audio file progress with ID {id.ToString()}");
            return progress;
        }
    }
}