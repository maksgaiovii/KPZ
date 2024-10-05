using AutoMapper;
using KPZ_Lab2.Models;
using Organizer.UI.Base;
using Organizer.UI.ViewModels;
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

        private DataViewModel _viewModel;

        private readonly IMapper _mapper;

        public App()
        {
            
            var mapping = new Mapping();
            _mapper = mapping.GetMapper(); 

            _model = DataModel.Load();
            _viewModel = _mapper.Map<DataModel,DataViewModel>(_model);

            var window = new MainWindow { DataContext = _viewModel };
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                _model=_mapper.Map<DataViewModel, DataModel>(_viewModel);
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
