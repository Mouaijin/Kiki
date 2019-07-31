using System;
using System.Collections.Generic;
using Kiki.Models.Data;

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
        public List<AudioBook> Books { get; set; }
    }
}