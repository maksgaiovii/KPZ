using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    [Serializable]
    public class PrintingHouse
    {
        [Key]
        public int PrintingHouseId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        // Navigation property for the relationship with PrintingHouseBook
        public ICollection<PrintingHouseBook>? PrintingHouseBooks { get; set; }
    }
}
