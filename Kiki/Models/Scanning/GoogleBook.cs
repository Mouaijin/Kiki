using System;
using System.Collections.Generic;

namespace Kiki.Models.Scanning
{
    public class GoogleBook
    {
        public string       Title         { get; set; }
        public List<string> Authors       { get; set; }
        public string       Description   { get; set; }
        public string       ThumbnailLink { get; set; }
        public string       Language      { get; set; }
        public string       Publisher     { get; set; }
        public string       Published     { get; set; }
        public string       Category      { get; set; }


        public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
    }
}