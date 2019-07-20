using System;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models {
    public class BookAuthor
    {
        [Required]
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}