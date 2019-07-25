using System;
using System.Collections.Generic;
using System.Linq;
using Kiki.FileSys;
using Kiki.Models.Metadata;
using Kiki.Models.Scanning;

namespace Kiki.Models.Data
{
    public class AudioBook
    {
        public Guid Id { get; set; }

        public Guid? BookId { get; set; }

        public Book Book { get; set; }

        public string AudioBookDirectoryPath { get; set; }

        public List<AudioFile> Files { get; set; }

        public bool IsIdentified => BookId != null;
        public int  TrackCount   => Files.Count;

        public AudioBook()
        {
        }

        public AudioBook(ScanDirectory dir)
        {
            AudioBookDirectoryPath = dir.FullPath;
            Files = new List<AudioFile>();
            foreach (ScanFile file in dir.ScanFiles.Where(x => x.IsAudioFile))
            {
                Files.Add(new AudioFile(file));
            }

            Files.Sort((file1, file2) => file1.TrackNumber.CompareTo(file2.TrackNumber));
        }

        public AudioBook(ScanFile file)
        {
            Files = new List<AudioFile>();
            Files.Add(new AudioFile(file));
            AudioBookDirectoryPath = file.FullPath;
        }
    }
}