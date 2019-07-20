using System;
using System.Collections.Generic;

namespace Kiki.Models {
    public class Series
    {
        public Guid Id { get; set; }

        public List<SeriesAuthor> SeriesAuthors { get; set; }
        public List<Book>         Books         { get; set; }
    }
}