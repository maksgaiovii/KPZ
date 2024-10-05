using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.ViewModels
{
    class PrintingHouseViewModel : ViewModelBase
    {

        private int _printingHouseId;

        public int PrintingHouseId
        {
            get { return _printingHouseId; }
            set { _printingHouseId = value; OnPropertyChanged("PrintingHouseID"); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set { _adress = value; OnPropertyChanged("Adress"); }
        }
    }
}
