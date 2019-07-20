using System;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models {
    public class SeriesAuthor
    {
        [Required]
        public Guid SeriesId { get; set; }

        public Series Series { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}