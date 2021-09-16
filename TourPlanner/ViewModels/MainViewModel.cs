﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using log4net;
using TourPlanner.BLL;
using TourPlanner.Models;
using TourPlanner.Views;


namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAppManagerFactory _appManagerFactory;
        private TourItem _currentTour;
        private LogItem _currentLog;
        private string _search;


        public ObservableCollection<TourItem> Tours { get; set; }
        public ObservableCollection<LogItem> Logs { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ICommand CreateTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; set; }
        public ICommand CopyTourCommand { get; set; }
        public ICommand UpdateTourCommand { get; set; }

        public ICommand CreateLogCommand { get; set; }
        public ICommand DeleteLogCommand { get; set; }
        public ICommand UpdateLogCommand { get; set; }

        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand PrintTourCommand { get; set; }
        public ICommand PrintAllCommand { get; set; }


        public string Search
        {
            get => _search;
            set
            {
                if (_search != value)
                {
                    _search = value;
                    RaisePropertyChangedEvent(nameof(Search));
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
                    _currentTour = value;
                    Logs.Clear();
                    FillLogsListView();
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


        public MainViewModel(IAppManagerFactory appManagerFactory)
        {
            this._appManagerFactory = appManagerFactory;
            Tours = new ObservableCollection<TourItem>();

            this.SearchCommand = new RelayCommand(o =>
            {
                try
                {
                    FillToursListView();
                    List<TourItem> tours = appManagerFactory.SearchForTours(Search).ToList();

                    foreach (TourItem item in Tours)
                    {
                        if (appManagerFactory.SearchForLogs(item, Search))
                        {
                            tours.Add(item);
                        }
                    }

                    tours = tours.Distinct().ToList();

                    Tours.Clear();
                    Logs.Clear();
                    foreach (TourItem item in tours)
                    {
                        Tours.Add(item);
                    }

                    log.Info("SearchCommand successful");
                }
                catch (Exception ex)
                {
                    log.Error("SearchCommand error. Exception: " + ex.Message);
                }

            });

            this.ClearCommand = new RelayCommand(o => {
                Search = "";

                FillToursListView();
                FillLogsListView();
            });

            this.DeleteTourCommand = new RelayCommand(o =>
            {
                Tours.Remove(appManagerFactory.DeleteTour(CurrentTour));
                IEnumerable<LogItem> logsOfCurrentTour = Logs.Where(x => x.TourItem.Id.Equals(CurrentTour.Id));
                
                foreach (LogItem item in logsOfCurrentTour)
                {
                    if (CurrentTour != null)
                    {
                        appManagerFactory.DeleteLog(item);
                    }
                }
                FillToursListView();
                FillLogsListView();
            }); 
            
            this.UpdateTourCommand = new RelayCommand(o =>
            {
                var view = new UpdateTourWindow(this, CurrentTour);
                view.ShowDialog();
            });

            this.CopyTourCommand = new RelayCommand(o =>
            {

                TourItem copiedTour = appManagerFactory.CreateTour(CurrentTour);
                Tours.Add(copiedTour);

                IEnumerable<LogItem> logsOfCurrentTour = Logs.Where(x => x.TourItem.Id.Equals(CurrentTour.Id));

                foreach (LogItem item in logsOfCurrentTour)
                {
                    if (CurrentTour != null)
                    {
                        appManagerFactory.CreateLog(copiedTour, item.DateTime, item.Report, item.Distance, item.TotalTime, item.Rating, item.AverageSpeed, 
                            item.MaxSpeed, item.MinSpeed, item.AverageStepCount, item.BurntCalories);
                    }
                }
                FillToursListView();
                FillLogsListView();

            });

            this.CreateTourCommand = new RelayCommand(o =>
            {
                var view = new AddTourWindow(this);
                view.ShowDialog();
            });

            this.DeleteLogCommand = new RelayCommand(o =>
            {
                if (CurrentTour != null)
                {
                    Logs.Remove(appManagerFactory.DeleteLog(CurrentLog));
                }

            });

            this.UpdateLogCommand = new RelayCommand(o => 
            {
                var view = new UpdateLogWindow(this, CurrentTour, CurrentLog);
                view.ShowDialog();
            });

            this.CreateLogCommand = new RelayCommand(o =>
            {
                var view = new AddLogWindow(this, CurrentTour);
                view.ShowDialog();
            });

            this.ImportCommand = new RelayCommand(o =>
            {
                Tours.Add(appManagerFactory.ImportData());
            });

            this.ExportCommand = new RelayCommand(o =>
            {
                appManagerFactory.ExportData(CurrentTour);
            });

            this.PrintTourCommand = new RelayCommand(o =>
            {
                appManagerFactory.PrintTour(CurrentTour);
            });

            this.PrintAllCommand = new RelayCommand(o =>
            {
                appManagerFactory.PrintAll();
            });

            InitToursListView();
            InitLogsListView();
        }


        public void InitToursListView()
        {
            Tours = new ObservableCollection<TourItem>();
            FillToursListView();
        }

        public void FillToursListView()
        {
            Tours.Clear();
            foreach (TourItem item in this._appManagerFactory.GetTours())
            {
                Tours.Add(item);
            }
        }
        
        public void InitLogsListView()
        {
            Logs = new ObservableCollection<LogItem>();
            FillLogsListView();
        }

        public void FillLogsListView()
        {
            Logs.Clear();
            if(CurrentTour != null)
                foreach (LogItem item in this._appManagerFactory.GetLogs(CurrentTour))
                {
                    Logs.Add(item);
                }
        }
    }
}
