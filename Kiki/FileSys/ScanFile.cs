using System;
using System.IO;

namespace Kiki.FileSys
{
    public class ScanFile
    {
        public ScanFile(FileInfo fi)
        {
            FullPath      = fi.FullName;
            FileName      = fi.Name.Substring(0, fi.Name.IndexOf('.'));
            FileExtension = fi.Extension.Substring(1);
        }

        public string FullPath      { get; set; }
        public string FileName      { get; set; }
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
    }
}