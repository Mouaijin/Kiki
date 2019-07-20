using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kiki.Models {
    public class AudioBook
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public List<AudioFile> Files { get; set; }
    }
}