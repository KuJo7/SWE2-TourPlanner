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
    class AddTourViewModel : ViewModelBase
    {
        private readonly IAppManagerFactory _appManagerFactory;
        private MainViewModel _mainWindow;
        private readonly Window _window;

        private string _Name = "";
        private string _Description = "";
        private string _From = "";
        private string _To = "";

        public ICommand AddTourCommand { get; set; }
        public ICommand CancelAddTourCommand { get; set; }

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

        public string From
        {
            get => _From;
            set
            {
                if (_From != value)
                {
                    _From = value;
                    RaisePropertyChangedEvent(nameof(From));
                }
            }
        }

        public string To
        {
            get => _To;
            set
            {
                if (_To != value)
                {
                    _To = value;
                    RaisePropertyChangedEvent(nameof(To));
                }
            }
        }


        public AddTourViewModel(Window window, MainViewModel mainWindow)
        {
            this._appManagerFactory = AppManagerFactory.GetFactoryManager();
            this._mainWindow = mainWindow;
            this._window = window;

            this.AddTourCommand = new RelayCommand(o =>
            {
                var tour = _appManagerFactory.CreateTour(Name, Description, From, To);
                if (tour != null)
                {
                    mainWindow.Tours.Add(tour);
                    _window?.Close();
                }
                else
                {
                    MessageBox.Show("Could not Add Tour.");
                    _window?.Close();
                }

            });

            this.CancelAddTourCommand = new RelayCommand(o =>
            {
                _window?.Close();
            });
        }
    }
}
