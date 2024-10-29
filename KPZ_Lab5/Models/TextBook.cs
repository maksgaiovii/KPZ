using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    public class TextBook
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Text")]
        public int TextId { get; set; }
        public Text Text { get; set; }
    }
}
