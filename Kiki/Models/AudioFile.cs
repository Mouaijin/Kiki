using System;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models {
    public class AudioFile
    {
        public Guid AudioFileId { get; set; }

        [Required]
        public Guid AudioBookId { get; set; }

        public AudioBook AudioBook { get; set; }

        [Required]
        public int TrackNumber { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public string Name { get; set; }
    }
}