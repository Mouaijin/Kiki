using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kiki.Models;

namespace Kiki.FileSys
{
    public class ScanDirectory
    {
        public string         FullPath  { get; set; }
        public List<ScanFile> ScanFiles { get; set; }

        public AudioBook ToAudioBook()
        {
            return new AudioBook(this);
        }

        public ScanDirectory(DirectoryInfo directoryInfo)
        {
            FullPath  = directoryInfo.FullName;
            ScanFiles = directoryInfo.GetFiles().Select(x => new ScanFile(x)).ToList();
        }
    }
}