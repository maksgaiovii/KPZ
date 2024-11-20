using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    [Serializable]
    public class Text
    {
        [Key]
        public int TextId { get; set; }

        [Required, MaxLength(40)]
        public string AuthorName { get; set; }

        [Required, MaxLength(40)]
        public string AuthorSurname { get; set; }

        [Required]
        public DateTime ReceiptDate { get; set; }

        [Required, MaxLength(80)]
        public string Title { get; set; }

        // Navigation property for the relationship with TextBook
        public ICollection<TextBook>? TextBooks { get; set; }
    }
}
