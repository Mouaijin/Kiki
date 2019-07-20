using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models
{
    public class AudioBookProgress
    {
        public Guid Id { get; set; }

        [Required] public Guid UserId { get; set; }
        public KikiUser User { get; set; }

        [Required] public Guid AudioBookId { get; set; }
        public AudioBook AudioBook { get; set; }

        [Required] public bool IsFinished { get; set; }

        public int CurrentTrack { get; set; }

        public List<AudioFileProgress> FileProgresses { get; set; }
    }
}