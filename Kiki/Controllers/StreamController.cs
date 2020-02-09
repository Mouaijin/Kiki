using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Logging;
using Kiki.Models;
using Kiki.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kiki.Controllers
{
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly KikiContext               _context;
        private readonly ILogger<StreamController> _logger;

        public StreamController(KikiContext context, ILogger<StreamController> logger)
        {
            _context = context;
            _logger  = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("api/stream/file")]
        public async Task<FileStreamResult> File([FromQuery] Guid id)
        {
            AudioFile file = await _context.AudioFiles.SingleOrDefaultAsync(x => x.Id == id);
            if (file == null)
            {
                _logger.LogDebug($"Missed audio file lookup for {id.ToString()} in /stream/file");
                return null;
            }

            try
            {
                using (FileStream stream = System.IO.File.OpenRead(file.Path))
                {
                    return File(stream, file.MimeType, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not open file for reading: {id.ToString()} - {file.Path}");
                return null;
            }
        }
        
        
    }
}