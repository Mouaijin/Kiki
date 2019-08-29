using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Data;
using Kiki.Models.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AudioFileProgress>> Progress([FromBody] AudioFileProgressUpdateRequest request)
        {
            if (request.ProgressID.HasValue)
            {
                AudioFileProgress progress =
                    await _context.FileProgresses.SingleOrDefaultAsync(x => x.Id == request.ProgressID.Value &&
                                                                            x.UserId == User.GetUserId());
                progress.Progress   = TimeSpan.FromTicks(request.Ticks);
                progress.IsFinished = request.IsFinished;
                EntityEntry<AudioFileProgress> updateEntity = _context.FileProgresses.Update(progress);
                await _context.SaveChangesAsync();
                return updateEntity.Entity;
            }

            AudioBookProgress bookProgress =
                await _context.BookProgresses.SingleOrDefaultAsync(x => x.AudioBookId == request.AudioBookID &&
                                                                        x.UserId == User.GetUserId());
            if (bookProgress == null)
            {
                var audioFileInfo =
                    await _context.AudioFiles.SingleOrDefaultAsync(x => x.Id == request.AudioFileID);
                if (audioFileInfo == null)
                {
                    _logger.LogError($"Couldn't create progress for file without database entry");
                    return BadRequest("The requested audio file could not be found");
                }

                EntityEntry<AudioBookProgress> bookProgressEntity =
                    await _context.BookProgresses.AddAsync(new AudioBookProgress()
                                                           {
                                                               AudioBookId  = request.AudioBookID,
                                                               CurrentTrack = audioFileInfo.TrackNumber,
                                                               IsFinished   = false,
                                                               UserId       = User.GetUserId(),
                                                               FileProgresses = new List<AudioFileProgress>()
                                                                                {
                                                                                    new AudioFileProgress()
                                                                                    {
                                                                                        UserId = User.GetUserId(),
                                                                                        AudioFileId =
                                                                                            request.AudioFileID,
                                                                                        IsFinished =
                                                                                            request.IsFinished,
                                                                                        Progress = TimeSpan
                                                                                            .FromTicks(request
                                                                                                           .Ticks)
                                                                                    }
                                                                                }
                                                           });
                await _context.SaveChangesAsync();
                return bookProgressEntity.Entity.FileProgresses.First();
            }

            EntityEntry<AudioFileProgress> progressEntity =
                await _context.FileProgresses.AddAsync(new AudioFileProgress()
                                                       {
                                                           UserId         = User.GetUserId(),
                                                           AudioFileId    = request.AudioFileID,
                                                           IsFinished     = request.IsFinished,
                                                           Progress       = TimeSpan.FromTicks(request.Ticks),
                                                           BookProgressId = bookProgress.Id
                                                       });
            await _context.SaveChangesAsync();
            return progressEntity.Entity;
        }
    }
}