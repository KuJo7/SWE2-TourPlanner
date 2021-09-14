using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BLL;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class UpdateTourViewModel : ViewModelBase
    {
        private readonly IAppManagerFactory _appManagerFactory;
        private MainViewModel _mainWindow;
        private readonly Window _window;

        private TourItem _currentTour;
        private string _Name = "";
        private string _Description = "";

        public ICommand UpdateTourCommand { get; set; }
        public ICommand CancelUpdateTourCommand { get; set; }

        public TourItem CurrentTour
        {
            get => _currentTour;
            set
            {
                if ((_currentTour != value) && (value != null))
                {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                }
            }
        }

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChangedEvent(nameof(Name));
                }
            }
        }

        public string Description
        {
            get => _Description;
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChangedEvent(nameof(Description));
                }
            }
        }

        public UpdateTourViewModel(Window window, TourItem currentTour, MainViewModel mainWindow)
        {
            this._appManagerFactory = AppManagerFactory.GetFactoryManager();
            this._mainWindow = mainWindow;
            this._window = window;
            this.CurrentTour = currentTour;

            this.UpdateTourCommand = new RelayCommand(o =>
            {
                CurrentTour.Name = Name;
                CurrentTour.Description = Description;

                mainWindow.Tours.Clear();
                mainWindow.Tours.Add(_appManagerFactory.UpdateTour(CurrentTour));
                mainWindow.FillToursListView();
                _window?.Close();
            });

            this.CancelUpdateTourCommand = new RelayCommand(o =>
            {
                _window?.Close();
            });
        }
    }
}
