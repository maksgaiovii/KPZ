using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    [Serializable]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required, MaxLength(80)]
        public string BookTitle { get; set; }

        public int NumberOfPages { get; set; }

        [MaxLength(25)]
        public string Genre { get; set; }

        [MaxLength(2)]
        public string LanguageCode { get; set; }

        [Required]
        public BookStatus BookStatus { get; set; }

        // Navigation Properties
        public ICollection<ContributorHistory> ContributorHistories { get; set; }
        public ICollection<PrintingHouseBook> PrintingHouseBooks { get; set; }
        public ICollection<TextBook> TextBooks { get; set; }
    }

    public enum BookStatus
    {
        Pending,
        Editing,
        Illustrating,
        CoverDesigning,
        InProgress,
        Printing,
        Completed
    }
}
