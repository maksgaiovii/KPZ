using KPZ_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.ViewModels
{
    class TeamMemberViewModel : ViewModelBase
    {
        private int _teamMemberId;

        public int TeamMemberId
        {
            get { return _teamMemberId; }
            set { _teamMemberId = value; OnPropertyChanged("TeamMemberId"); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _surname;

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged("Surname"); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }

        private Role _role;

        public Role Role
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged("Role"); }
        }


    }
}
