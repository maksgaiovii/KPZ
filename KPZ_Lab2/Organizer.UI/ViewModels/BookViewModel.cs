using KPZ_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.ViewModels
{
    class BookViewModel : ViewModelBase
    {

        private int _bookId;

        public int BookId
        {
            get { return _bookId; }
            set { _bookId = value; OnPropertyChanged("BookId"); }
        }

        private string _bookTitle;

        public string BookTitle
        {
            get { return _bookTitle; }
            set { _bookTitle = value; OnPropertyChanged("BookTitle"); }
        }

        private int _numberOfPages;

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set { _numberOfPages = value; OnPropertyChanged("NumberOfPages"); }
        }

        private string _genre;

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; OnPropertyChanged("Genre"); }
        }

        private string _languageCode;

        public string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; OnPropertyChanged("LanguageCode"); }
        }

        private BookStatus _bookStatus;

        public BookStatus BookStatus
        {
            get { return _bookStatus; }
            set { _bookStatus = value; OnPropertyChanged("BookStatus"); }
        }
    }
}
