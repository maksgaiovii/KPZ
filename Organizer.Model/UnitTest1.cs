using KPZ_Lab2.Models;
using KPZ_Lab2.Serialization;
using Xunit;


namespace Lab2Test
{
    public class UnitTest1
    {
        [Fact]
        public void Serialize_WhenDataIsValid_Success()
        {
            var model = new DataModel();
            model.Texts = new List<Text> { new Text { AuthorName = "Name", AuthorSurname = "Surname", ReceiptDate = new DateTime(2024, 01, 01), TextId = 1, Title = "Title" } };
            model.TeamMembers = new List<TeamMember> { new TeamMember { TeamMemberId = 1, Name = "name", Surname = "surname", Email = "email@gmail.com", Role = Role.Illustrator } };
            model.Books = new List<Book> { new Book { BookId = 1, BookTitle = "Title", NumberOfPages = 100, Genre = "Fantasy", LanguageCode = "en", BookStatus = BookStatus.InProgress } };
            model.PrintingHouses=new List<PrintingHouse> { new PrintingHouse{PrintingHouseID=1,Name="name",Address="4 Bandery Lviv"} };

            DataSerializer.SerializeData(@"C:\Users\Legion\source\repos\KPZ\KPZ_Lab2\SerializationTest.dat", model);
        }

        [Fact]
        public void Deserialize_WhenDataIsValid_Success()
        {
            var model = DataSerializer.DeserializeData(@"C:\Users\Legion\source\repos\KPZ\KPZ_Lab2\SerializationTest.dat");
        }
    }
}