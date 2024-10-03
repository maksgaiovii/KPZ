using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab2.Models
{
    [Serializable]
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int NumberOfPages { get; set; }
        public string Genre { get; set; }
        public string LanguageCode { get; set; }
        public BookStatus BookStatus { get; set; }
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
