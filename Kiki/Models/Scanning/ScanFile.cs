using System;
using System.IO;
using Kiki.Models.Data;
using File = TagLib.File;

namespace Kiki.Models.Scanning
{
    public class ScanFile
    {
        private AudioTags _tags;
        private bool      _scannedTags = false;

        public ScanFile(FileInfo fi)
        {
            FullPath        = fi.FullName;
            FileName        = fi.Name.Substring(0, fi.Name.IndexOf('.'));
            FileExtension   = fi.Extension.Substring(1);
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

        public AudioTags Tags
        {
            get
            {
                if (!_scannedTags)
                {
                    try
                    {
                        var tagFile = TagLib.File.Create(FullPath);
                        if (!tagFile.Tag.IsEmpty)
                        {
                            _tags = new AudioTags(tagFile.Tag);
                        }
                        else
                        {
                            _tags = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        //todo: logging
                        Console.Error.WriteLine($"Error parsing audio tag: {ex.Message}; {ex.StackTrace}");
                        _tags = null;
                    }

                    _scannedTags = true;
                }

                return _tags;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ScanFile file)
            {
                return file.FullPath == FullPath;
            }

            return false;
        }
    }
}