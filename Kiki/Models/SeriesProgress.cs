using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models
{
    public class SeriesProgress
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public KikiUser User { get; set; }

        [Required]
        public Guid SeriesId { get; set; }

        public Series Series { get; set; }

        [Required]
        public bool IsFinished { get; set; }

        public Guid              CurrentBookProgressId    { get; set; }
        public AudioBookProgress CurrentAudioBookProgress { get; set; }

        public List<AudioBookProgress> BookProgresses { get; set; }
    }
}