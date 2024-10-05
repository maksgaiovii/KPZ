using KPZ_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.ViewModels
{
    class DataViewModel : ViewModelBase
    {
        private string _visibleControl="Books";

        public string VisibleControl
        {
            get { return _visibleControl; }
            set { _visibleControl = value; OnPropertyChanged("VisibleControl"); }
        }

        private ObservableCollection<BookViewModel> _books;

        public ObservableCollection<BookViewModel> Books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged("Books"); }
        }


        private ObservableCollection<PrintingHouseViewModel> _printingHouses;

        public ObservableCollection<PrintingHouseViewModel> PrintingHouses
        {
            get { return _printingHouses; }
            set { _printingHouses = value; OnPropertyChanged("PrintingHouses"); }
        }

        private ObservableCollection<TeamMemberViewModel> _teamMembers;

        public ObservableCollection<TeamMemberViewModel> TeamMembers
        {
            get { return _teamMembers; }
            set { _teamMembers = value; OnPropertyChanged("TeamMembers"); }
        }

        private ObservableCollection<TextViewModel> _texts;
        public ObservableCollection<TextViewModel> Texts
        {
            get { return _texts; }
            set { _texts = value; OnPropertyChanged("Texts"); }
        }
    }
}
