using KPZ_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.ViewModels
{
    class DataViewModel
    {

        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<PrintingHouse> PrintingHouses { get; set; }

        public ObservableCollection<TeamMember> TeamMembers { get; set; }

        public ObservableCollection<Text> Texts { get; set; }
    }
}
