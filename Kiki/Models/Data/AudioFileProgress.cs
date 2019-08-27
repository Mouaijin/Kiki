using System;
using System.ComponentModel.DataAnnotations;
using Kiki.Models.Identity;

namespace Kiki.Models.Data
{
    public class AudioFileProgress
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public KikiUser User { get; set; }

        [Required]
        public Guid AudioFileId { get; set; }

        public AudioFile File { get; set; }

        [Required]
        public Guid BookProgressId { get; set; }

        public AudioBookProgress AudioBookProgress { get; set; }

        [Required]
        public bool IsFinished { get; set; }

        [Required]
        public TimeSpan Progress { get; set; }
    }
}