using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using TourPlanner.BLL;
using TourPlanner.Models;


namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAppManagerFactory _tourManager;
        private TourItem _currentTour;
        private string _searchName;
        private bool _isSelected;

        public ObservableCollection<TourItem> Tours { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand CopyCommand { get; set; }


        public string SearchName
        {
            get => _searchName;
            set
            {
                if (_searchName != value)
                {
                    _searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        public TourItem CurrentTour
        {
            get => _currentTour;
            set
            {
                if ((_currentTour != value) && (value != null))
                {
                    _isSelected = true;
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                }
                else
                {
                    _isSelected = false;
                }
            }
        }

        public MainViewModel(IAppManagerFactory tourManager)
        {
            this._tourManager = tourManager;
            Tours = new ObservableCollection<TourItem>();

            this.SearchCommand = new RelayCommand(o =>
            {
                IEnumerable<TourItem> tours = tourManager.SearchForTours(SearchName);
                Tours.Clear();
                foreach(TourItem item in tours)
                {
                    Tours.Add(item);
                }
            },
            o =>
            {
                Debug.Print("CanExecute SearchCommand");
                return !string.IsNullOrEmpty(_searchName);
            });

            this.ClearCommand = new RelayCommand(o => {
                Tours.Clear();
                SearchName = "";

                FillListView();
            },
            o =>
            {
                Debug.Print("CanExecute ClearCommand");
                return !string.IsNullOrEmpty(_searchName);
            });

            this.DeleteCommand = new RelayCommand(o =>
            {

                tourManager.DeleteTour(CurrentTour);


            },
                o =>
            {
                Debug.Print("CanExecute DeleteCommand");

                return _isSelected;
            });

            this.UpdateCommand = new RelayCommand(o =>
                {

                    tourManager.UpdateTour(CurrentTour, "newTestName", "newRoutInfoTest", "newTestDescription", 50.0);



                },
                o =>
                {
                    Debug.Print("CanExecute UpdateCommand");

                    return _isSelected;
                });

            this.CopyCommand = new RelayCommand(o =>
                {

                    Tours.Add(tourManager.CopyTour(CurrentTour, "newTestName", "newRoutInfoTest", "new TestDescription"));



                },
                o =>
                {
                    Debug.Print("CanExecute CopyCommand");

                    return _isSelected;
                });

            this.AddCommand = new RelayCommand(o =>
                {
                    Tours.Add(tourManager.AddTour(new TourItem(){Name = "TEST"}));

                });

            InitListView();

        }

        public void InitListView()
        {
            Tours = new ObservableCollection<TourItem>();
            FillListView();
        }

        private void FillListView()
        {
            foreach (TourItem item in _tourManager.GetTours())
            {
                Tours.Add(item);
            }
        }
    }
}
