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
                //Attempt to group directory's files into albums
                IEnumerable<IGrouping<string, ScanFile>> albumGroups = dir.ScanFiles.GroupBy(x => x.Tags.AlbumName);
                foreach (var albumGroup in albumGroups)
                {
                    //Assume that groups of files with the same album are an audiobook
                    if (albumGroup.Count() > 1)
                    {
                        //remove from pool of files remaining in directory
                        dir.ScanFiles.RemoveAll(x => albumGroup.Contains(x));
                        yield return new AudioBook(new ScanDirectory(dir.FullPath, albumGroup.ToList()));
                    }
                }

                //only return remaining files as audiobook if the album grouping method missed some
                if (dir.ScanFiles.Count > 0)
                {
                    yield return dir.ToAudioBook();
                }
            }

            //recurse into child directories
            foreach (DirectoryInfo childDirectory in directoryInfo.EnumerateDirectories())
            {
                //break at specified depth to prevent infinite loops and such
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