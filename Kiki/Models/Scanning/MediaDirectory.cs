using System;

namespace Kiki.Models.Scanning
{
    public class MediaDirectory
    {
        public Guid Id { get; set; }

        public string DirectoryPath { get; set; }
        public DateTime? LastScan { get; set; }
        
        
    }
}