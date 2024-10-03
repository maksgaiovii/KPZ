using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab2.Models
{
    [Serializable]
    public class Text
    {
        public int TextId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string Title { get; set; }
    }
}
