using System;

namespace Kiki.Models.Requests.Update
{
    public class AudioFileProgressUpdateRequest
    {
        public Guid? ProgressID { get; set; }
        public Guid AudioBookID { get; set; }
        public Guid AudioFileID { get; set; }
        public long Ticks { get; set; }
        public bool IsFinished { get; set; }
    }
}