using System;
using System.IO;
using File = TagLib.File;

namespace Kiki.Models.Scanning
{
    public class ScanFile
    {
        public ScanFile(FileInfo fi)
        {
            FullPath = fi.FullName;
            FileName = fi.Name.Substring(0, fi.Name.IndexOf('.'));
            FileExtension = fi.Extension.Substring(1);
            ParentDirectory = fi.DirectoryName;
        }

        ///Full path of file
        public string FullPath { get; set; }

        /// <summary>
        /// Name of file, without extension
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File extension, no leading period
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Full path of parent directory
        /// </summary>
        public string ParentDirectory { get; set; }

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

        /// <summary>
        /// Attempts to extract track information from media tags
        /// todo: Heuristic fallback is planned
        /// </summary>
        /// <returns>1-based index of the track in the audiobook/album</returns>
        public int GetTrackNumber()
        {
            try
            {
                var tagFile = TagLib.File.Create(FullPath);
                if (!tagFile.Tag.IsEmpty)
                {
                    return (int) tagFile.Tag.Track;
                }
            }
            catch (Exception ex)
            {
                //todo: logging
                Console.Error.WriteLine($"Error parsing audio tag: {ex.Message}; {ex.StackTrace}");
            }

            //todo: heuristic track detection
            return 1;
        }

        /// <summary>
        /// Attempts to extract track name from media tags, falling back on filename where impossible
        /// </summary>
        /// <returns>Name of track</returns>
        public string GetTrackName()
        {
            try
            {
                var tagFile = TagLib.File.Create(FullPath);
                if (!tagFile.Tag.IsEmpty)
                {
                    return tagFile.Tag.Title;
                }
            }
            catch (Exception ex)
            {
                //todo: logging
                Console.Error.WriteLine($"Error parsing audio tag: {ex.Message}; {ex.StackTrace}");
            }

            //todo: tag detection for other files
            return FileName;
        }

        public string GetAlbumName()
        {
            try
            {
                var tagFile = TagLib.File.Create(FullPath);
                if (!tagFile.Tag.IsEmpty)
                {
                    return tagFile.Tag.Album;
                }
            }
            catch (Exception ex)
            {
                //todo: logging
                Console.Error.WriteLine($"Error parsing audio tag: {ex.Message}; {ex.StackTrace}");
            }

            //todo: tag detection for other files
            return FileName;
        }

        public override bool Equals(object obj)
        {
            if (obj is ScanFile file)
            {
                return file.FullPath == FullPath;
            }

            return false;
        }

        // public void GetAllMetaDataTags(File file)
        // {
        //      file.Properties.
        // }
    }
}