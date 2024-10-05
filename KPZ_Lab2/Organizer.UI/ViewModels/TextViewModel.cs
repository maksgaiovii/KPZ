using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.ViewModels
{
    class TextViewModel : ViewModelBase
    {
        private int _textId;

        public int TextId
        {
            get { return _textId; }
            set { _textId = value; OnPropertyChanged("TextId"); }
        }

        private string _authorName;

        public string AuthorName
        {
            get { return _authorName; }
            set { _authorName = value; OnPropertyChanged("AuthorName"); }
        }

        private string _authorSurname;

        public string AuthorSurname
        {
            get { return _authorSurname; }
            set { _authorSurname = value; OnPropertyChanged("AuthorSurname"); }
        }

        private DateTime _receiptDate;

        public DateTime ReceiptDate
        {
            get { return _receiptDate; }
            set { _receiptDate = value; OnPropertyChanged("ReceiptDate"); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title");}
        }
    }
}