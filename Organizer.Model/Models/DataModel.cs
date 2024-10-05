using KPZ_Lab2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab2.Models
{

    [Serializable]
    public class DataModel 
    {
        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<PrintingHouse> PrintingHouses { get; set; }

        public IEnumerable<TeamMember> TeamMembers { get; set; }

        public IEnumerable<Text> Texts { get; set; }

        public static string DataPath = @"C:\Users\Legion\source\repos\KPZ\Organizer.Model\Models\organizer.dat";

        public static DataModel Load()
        {
            if (File.Exists(DataPath))
            {
             return DataSerializer.DeserializeData(DataPath);
            }
            return new DataModel();
        }

        public  void Save()
        {
            DataSerializer.SerializeData(DataPath, this);
        }
    }
}
