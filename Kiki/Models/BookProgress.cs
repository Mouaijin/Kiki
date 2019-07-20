using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models
{
    public class BookProgress
    {
        public Guid Id { get; set; }

        [Required] public Guid UserId { get; set; }
        public KikiUser User { get; set; }

        [Required] public Guid BookId { get; set; }
        public Book Book { get; set; }

        [Required] public bool IsFinished { get; set; }

        public Guid CurrentFileProgressId { get; set; }
        public AudioFileProgress CurrentAudioFileProgress { get; set; }

        public List<AudioFileProgress> FileProgresses { get; set; }
    }
}