using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models {
    public class Author
    {
        public Guid AuthorId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<SeriesAuthor> Series { get; set; }
        public List<BookAuthor>   Books  { get; set; }
    }
}