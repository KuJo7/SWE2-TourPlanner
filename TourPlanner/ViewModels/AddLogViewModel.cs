using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BLL.Factory;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class AddLogViewModel : ViewModelBase
    {
        private readonly IAppManagerFactory _appManagerFactory;
        private MainViewModel _mainWindow;
        private readonly Window _window;

        private TourItem _currentTour;
        private DateTime _DateTime = DateTime.Now;
        private TimeSpan _TotalTime;
        private string _Report = "";
        private int _Distance;
        private int _Rating;
        private int _AverageSpeed;
        private int _MaxSpeed;
        private int _MinSpeed;
        private int _AverageStepCount;
        private int _BurntCalories;

        public ICommand AddLogCommand { get; set; }
        public ICommand CancelAddLogCommand { get; set; }

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

        public DateTime DateTime
        {
            get
            {
                return _DateTime;
            }
            set
            {
                if (_DateTime != value)
                {
                    _DateTime = value;
                    RaisePropertyChangedEvent(nameof(DateTime));
                }
            }
        }
        public TimeSpan TotalTime
        {
            get
            {
                return _TotalTime;
            }
            set
            {
                if (_TotalTime != value)
                {
                    _TotalTime = value;
                    RaisePropertyChangedEvent(nameof(TotalTime));
                }
            }
        }
        public string Report
        {
            get
            {
                return _Report;
            }
            set
            {
                if (_Report != value)
                {
                    _Report = value;
                    RaisePropertyChangedEvent(nameof(Report));
                }
            }
        }
        public int Distance
        {
            get
            {
                return _Distance;
            }
            set
            {
                if (_Distance != value)
                {
                    _Distance = value;
                    RaisePropertyChangedEvent(nameof(Distance));
                }
            }
        }
        public int Rating
        {
            get
            {
                return _Rating;
            }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    RaisePropertyChangedEvent(nameof(Rating));
                }
            }
        }
        public int AverageSpeed
        {
            get
            {
                return _AverageSpeed;
            }
            set
            {
                if (_AverageSpeed != value)
                {
                    _AverageSpeed = value;
                    RaisePropertyChangedEvent(nameof(AverageSpeed));
                }
            }
        }
        public int MaxSpeed
        {
            get
            {
                return _MaxSpeed;
            }
            set
            {
                if (_MaxSpeed != value)
                {
                    _MaxSpeed = value;
                    RaisePropertyChangedEvent(nameof(MaxSpeed));
                }
            }
        }
        public int MinSpeed
        {
            get
            {
                return _MinSpeed;
            }
            set
            {
                if (_MinSpeed != value)
                {
                    _MinSpeed = value;
                    RaisePropertyChangedEvent(nameof(MinSpeed));
                }
            }
        }
        public int AverageStepCount
        {
            get
            {
                return _AverageStepCount;
            }
            set
            {
                if (_AverageStepCount != value)
                {
                    _AverageStepCount = value;
                    RaisePropertyChangedEvent(nameof(AverageStepCount));
                }
            }
        }
        public int BurntCalories
        {
            get
            {
                return _BurntCalories;
            }
            set
            {
                if (_BurntCalories != value)
                {
                    _BurntCalories = value;
                    RaisePropertyChangedEvent(nameof(BurntCalories));
                }
            }
        }

        public AddLogViewModel(Window window, TourItem  currentTour, MainViewModel mainWindow)
        {
            this._appManagerFactory = AppManagerFactory.GetFactoryManager();
            this._mainWindow = mainWindow;
            this._window = window;
            this.CurrentTour = currentTour;

            this.AddLogCommand = new RelayCommand(o =>
            {
                if (CurrentTour != null)
                    mainWindow.Logs.Add(_appManagerFactory.CreateLog(CurrentTour, DateTime, Report, Distance, TotalTime, Rating, AverageSpeed, MaxSpeed, MinSpeed, 
                        AverageStepCount, BurntCalories));

                _window?.Close();
            });

            this.CancelAddLogCommand = new RelayCommand(o =>
            {
                _window?.Close();
            });
        }
    }
}
