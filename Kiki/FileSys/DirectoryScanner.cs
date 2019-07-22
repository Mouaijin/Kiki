using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (dir.ScanFiles.Count > 0)
            {
                yield return dir.ToAudioBook();
            }

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
}