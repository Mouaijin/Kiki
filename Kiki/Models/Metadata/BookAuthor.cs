using System;
using System.ComponentModel.DataAnnotations;
using Kiki.Models.Data;

namespace Kiki.Models.Metadata
{
    /// <summary>
    /// Many-many join entity between Book and Author (to accomodate multiple authors per book, and many books per author)
    /// </summary>
    public class BookAuthor
    {
        [Required]
        public Guid BookId { get; set; }

        public AudioBook Book { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}