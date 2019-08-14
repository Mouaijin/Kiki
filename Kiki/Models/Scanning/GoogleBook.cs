using System;
using System.Collections.Generic;

namespace Kiki.Models.Scanning
{
    /// <summary>
    /// Data object used to bridge between Google Books API objects and internal scanning functionality more cleanly
    /// </summary>
    public class GoogleBook
    {
        public string       Title         { get; set; }
        public List<string> Authors       { get; set; }
        public string       Description   { get; set; }
        public string       ThumbnailLink { get; set; }
        public string       Language      { get; set; }
        public string       Publisher     { get; set; }
        public DateTime?    Published     { get; set; }
        public string       Category      { get; set; }
        public string       GoogleBooksID { get; set; }


        public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
    }
}