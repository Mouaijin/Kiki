using System;
using System.Collections.Generic;

namespace Kiki.Models {
    public class Book
    {
        public Guid Id { get; set; }

        public string Title       { get; set; }
        public Guid    Year        { get; set; }
        public Guid    SeriesEntry { get; set; }

        public Guid?   SeriesId { get; set; }
        public Series Series   { get; set; }

        public List<BookAuthor> Authors { get; set; }

        public bool IsPartOfSeries => SeriesId != null;
    }
}