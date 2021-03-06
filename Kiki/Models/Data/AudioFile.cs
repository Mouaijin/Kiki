using System;
using System.ComponentModel.DataAnnotations;
using Kiki.FileSys;
using Kiki.Models.Scanning;

namespace Kiki.Models.Data
{
    public class AudioFile
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AudioBookId { get; set; }

        public AudioBook AudioBook { get; set; }

        public Guid? AudioTagsId { get; set; }
        public AudioTags AudioTags { get; set; }


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

        [Required]
        public string MimeType { get; set; }

        public AudioFile() { }

        public AudioFile(ScanFile file)
        {
            Path          = file.FullPath;
            FileName      = file.FileName;
            FileExtension = file.FileExtension;
            Name = file.Tags?.Title ?? file.FileName;
            TrackNumber   = file.Tags?.Track ?? 1;
            AudioTags = file.Tags;
            MimeType = file.MimeType;
        }
    }
}