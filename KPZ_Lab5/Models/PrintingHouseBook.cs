using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    public class PrintingHouseBook
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("PrintingHouse")]
        public int PrintingHouseId { get; set; }
        public PrintingHouse PrintingHouse { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        [Required]
        public int BooksQuantity { get; set; }
    }
}
