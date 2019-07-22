using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Kiki.FileSys;

namespace Kiki.Models
{
    public class AudioBook
    {
        public Guid Id { get; set; }

        public Guid? BookId { get; set; }

        public Book Book { get; set; }

        public string DirectoryPath { get; set; }

        public List<AudioFile> Files { get; set; }

        public bool IsIdentified => BookId != null;
        public int TrackCount => Files.Count;

        public AudioBook()
        {
        }

        public AudioBook(ScanDirectory dir)
        {
            DirectoryPath = dir.FullPath;
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
            DirectoryPath = file.FullPath;
        }
    }
}