using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kiki.Models.Data;

namespace Kiki.Models.Scanning
{
    public class ScanDirectory
    {
        /// <summary>
        /// Full path of directory
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// List of scanned audio files in directory
        /// </summary>
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

        public ScanDirectory(string fullPath, List<ScanFile> scanFiles)
        {
            FullPath = fullPath;
            ScanFiles = scanFiles;
        }
    }
}