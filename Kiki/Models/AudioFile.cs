using System;
using System.ComponentModel.DataAnnotations;
using Kiki.FileSys;

namespace Kiki.Models
{
    public class AudioFile
    {
        public Guid Id { get; set; }

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

        public AudioFile() { }

        public AudioFile(ScanFile file)
        {
            Path          = file.FullPath;
            FileName      = file.FileName;
            FileExtension = file.FileExtension;
            Name          = file.GetTrackName();
            TrackNumber   = file.GetTrackNumber();
        }
    }
}