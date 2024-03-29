﻿using System;
using System.Windows;
using System.Windows.Input;
using log4net;
using TourPlanner.BLL.Factory;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class UpdateLogViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAppManagerFactory _appManagerFactory;
        private MainViewModel _mainWindow;
        private readonly Window _window;

        private TourItem _currentTour;
        private LogItem _currentLog;
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

        public ICommand UpdateLogCommand { get; set; }
        public ICommand CancelUpdateLogCommand { get; set; }

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

        public LogItem CurrentLog
        {
            get => _currentLog;
            set
            {
                if ((_currentLog != value) && (value != null))
                {
                    _currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
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

        public UpdateLogViewModel(Window window, TourItem currentTour, LogItem currentLog, MainViewModel mainWindow)
        {
            this._appManagerFactory = AppManagerFactory.GetFactoryManager();
            this._mainWindow = mainWindow;
            this._window = window;
            this.CurrentTour = currentTour;
            this.CurrentLog = currentLog;

            this.UpdateLogCommand = new RelayCommand(o =>
            {
                CurrentLog.DateTime = DateTime;
                CurrentLog.Report = Report;
                CurrentLog.Distance = Distance;
                CurrentLog.TotalTime = TotalTime;
                CurrentLog.Rating = Rating;
                CurrentLog.AverageSpeed = AverageSpeed;
                CurrentLog.MaxSpeed = MaxSpeed;
                CurrentLog.MinSpeed = MinSpeed;
                CurrentLog.AverageStepCount = AverageStepCount;
                CurrentLog.BurntCalories = BurntCalories;

                try
                {
                    if (CurrentTour != null)
                    {
                        _appManagerFactory.UpdateLog(CurrentLog);
                        mainWindow.FillLogsListView();
                        _window?.Close();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not Update Log.");
                    _window?.Close();
                    log.Error("UpdateLogCommand error. Exception: ", ex);
                }

            });

            this.CancelUpdateLogCommand = new RelayCommand(o =>
            {
                _window?.Close();
            });
        }
    }
}
