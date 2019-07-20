using System;
using System.Collections.Generic;

namespace Kiki.Models {
    public class Book
    {
        public Guid BookId { get; set; }

        public string Title       { get; set; }
        public int    Year        { get; set; }
        public int    SeriesEntry { get; set; }

        public Guid   SeriesId { get; set; }
        public Series Series   { get; set; }

        public List<BookAuthor> Authors { get; set; }
    }
}