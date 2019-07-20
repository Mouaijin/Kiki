using System;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models
{
    public class PlayHistoryEntry
    {
        public Guid Id { get; set; }

       [Required] public Guid UserId { get; set; }
       public KikiUser User { get; set; }
       
       [Required] public Guid FileProgressId { get; set; }
       public AudioFileProgress AudioFileProgress { get; set; }

       public DateTime StartTime { get; set; }
       public string IP { get; set; }
    }
}