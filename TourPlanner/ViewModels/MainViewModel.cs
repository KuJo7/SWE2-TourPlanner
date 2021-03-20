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
        private IAppManager tourManager;
        private TourItem currentTour;
        private string searchName;

        public ObservableCollection<TourItem> Tours { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand ClearCommand { get; set; }


        public string SearchName
        {
            get { return searchName; }
            set
            {
                if (searchName != value)
                {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        public TourItem CurrentTour
        {
            get { return currentTour; }
            set
            {
                if ((currentTour != value) && (value != null))
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                }
            }
        }

        public MainViewModel(IAppManager tourManager)
        {
            this.tourManager = tourManager;
            Tours = new ObservableCollection<TourItem>();

            this.SearchCommand = new RelayCommand(o =>
            {
                IEnumerable<TourItem> tours = tourManager.SearchForTours(SearchName);
                Tours.Clear();
                foreach(TourItem item in tours)
                {
                    Tours.Add(item);
                }
            });

            this.ClearCommand = new RelayCommand(o => {
                Tours.Clear();
                SearchName = "";

                FillListView();
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
            foreach (TourItem item in tourManager.GetTours())
            {
                Tours.Add(item);
            }
        }

    }
}
