using System;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models.Metadata
{
    /// <summary>
    /// Many-many join entity between Series and Author (to accomodate multiple authors per series, and many series per author)
    /// </summary>
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