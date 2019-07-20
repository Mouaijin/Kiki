using System;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models
{
    public class AudioFileProgress
    {
        public Guid Id { get; set; }

        [Required] public Guid UserId { get; set; }
        public KikiUser User { get; set; }

        [Required] public Guid AudioFileId { get; set; }
        public AudioFile File { get; set; }

        [Required] public Guid BookProgressId { get; set; }
        public AudioBookProgress AudioBookProgress { get; set; }

        [Required] public bool IsFinished { get; set; }
        [Required] public DateTime Progress { get; set; }
    }
}