using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models.Metadata
{
    public class Author
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<SeriesAuthor> Series { get; set; }
        public List<BookAuthor>   Books  { get; set; }
    }
}