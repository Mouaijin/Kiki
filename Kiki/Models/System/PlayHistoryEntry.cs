using System;
using System.ComponentModel.DataAnnotations;
using Kiki.Models.Data;
using Kiki.Models.Identity;

namespace Kiki.Models.System
{
    public class PlayHistoryEntry
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public KikiUser User { get; set; }

        [Required]
        public Guid FileProgressId { get; set; }

        public AudioFileProgress AudioFileProgress { get; set; }

        public DateTime StartTime { get; set; }
        public string   IP        { get; set; }
    }
}