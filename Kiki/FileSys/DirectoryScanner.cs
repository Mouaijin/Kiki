using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Id3;
using Kiki.Models;

namespace Kiki.FileSys
{
    public class DirectoryScanner
    {
        private const int MaxRecursionDepth = 20;

        public List<AudioBook> ScanForBooks(string path)
        {
            return ScanForBooks(new DirectoryInfo(path)).ToList();
        }

        public IEnumerable<AudioBook> ScanForBooks(DirectoryInfo directoryInfo, int depth = 0)
        {
            ScanDirectory dir = new ScanDirectory(directoryInfo);
            yield return dir.ToAudioBook();
            foreach (DirectoryInfo childDirectory in directoryInfo.EnumerateDirectories())
            {
                if (depth == MaxRecursionDepth)
                    yield break;
                foreach (AudioBook audioBook in ScanForBooks(childDirectory, depth + 1))
                {
                    yield return audioBook;
                }
            }
        }
    }

    public class ScanDirectory
    {
        public string FullPath { get; set; }
        public List<ScanFile> ScanFiles { get; set; }

        public AudioBook ToAudioBook()
        {
            return new AudioBook(this);
        }

        public ScanDirectory(DirectoryInfo directoryInfo)
        {
            FullPath = directoryInfo.FullName;
            ScanFiles = directoryInfo.GetFiles().Select(x => new ScanFile(x)).ToList();
        }
    }

    public class ScanFile
    {
        public ScanFile(FileInfo fi)
        {
            FullPath = fi.FullName;
            FileName = fi.Name.Substring(0, fi.Name.IndexOf('.'));
            FileExtension = fi.Extension.Substring(1);
        }

        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public bool IsAudioFile
        {
            get
            {
                switch (FileExtension)
                {
                    case "mp3":
                    case "aac":
                    case "ogg":
                    case "flac":
                    case "wav":
                        return true;
                    default: return false;
                }
            }
        }


        public int GetTrackNumber()
        {
            if (FileExtension == "mp3")
            {
                using (var mp3 = new Mp3(FullPath))
                {
                    foreach (Id3Tag tag in mp3.GetAllTags())
                    {
                        if (tag.Track.IsAssigned)
                        {
                            return tag.Track.Value;
                        }
                    }
                }
            }

            //todo: heuristic track detection, tag detection for other files
            return 1;
        }

        public string GetTrackName()
        {
            if (FileExtension == "mp3")
            {
                using (var mp3 = new Mp3(FullPath))
                {
                    foreach (Id3Tag tag in mp3.GetAllTags())
                    {
                        if (tag.Title.IsAssigned)
                        {
                            return tag.Title.Value;
                        }
                    }
                }
            }

            //todo: tag detection for other files
            return FileName;
        }
    }
}