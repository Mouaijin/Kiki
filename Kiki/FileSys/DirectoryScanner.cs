using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kiki.Models;
using Kiki.Models.Data;
using Kiki.Models.Scanning;

namespace Kiki.FileSys
{
    public class DirectoryScanner
    {
        private const int MaxRecursionDepth = 20;

        /// <summary>
        /// Scan for audiobooks recursively from start directory up to the maximum recursion depth (20)
        /// Warning: This will recognize each directory as an audiobook with the files therein as tracks, it will not detect individual files as individual books
        /// </summary>
        /// <param name="path">Starting directory path</param>
        /// <returns>List of detected audiobook folders with each audio file therein as a track</returns>
        public List<AudioBook> ScanForBooks(string path)
        {
            return ScanForBooks(new DirectoryInfo(path)).ToList();
        }

        /// <summary>
        /// Scan for audiobooks recursively from start directory up to the maximum recursion depth (20)
        /// Warning: This will recognize each directory as an audiobook with the files therein as tracks, it will not detect individual files as individual books
        /// </summary>
        /// <param name="directoryInfo">Starting directory information</param>
        /// <param name="depth">Current recursion depth, should be set to default (0) expect for recursive calls</param>
        /// <returns>Collection of detected audiobook folders with each audio file therein as a track</returns>
        private IEnumerable<AudioBook> ScanForBooks(DirectoryInfo directoryInfo, int depth = 0)
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