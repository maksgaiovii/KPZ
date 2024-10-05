using KPZ_Lab2.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Organizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DataModel _model;

        public App()
        {
            _model = DataModel.Load();
            var window = new MainWindow { DataContext = _model };
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                _model.Save();
            }
            catch (Exception)
            {
                base.OnExit(e);
                throw;
            }
        }
    }

}
