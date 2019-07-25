using System;
using System.Collections.Generic;

namespace Kiki.Models.Metadata
{
    public class Series
    {
        public Guid Id { get; set; }

        /// <summary>
        /// List of authors for the series
        /// </summary>
        public List<SeriesAuthor> SeriesAuthors { get; set; }

        /// <summary>
        /// List of books in series
        /// </summary>
        public List<Book> Books { get; set; }
    }
}