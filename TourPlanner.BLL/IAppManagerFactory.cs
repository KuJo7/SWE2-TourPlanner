using System;
using System.Collections.Generic;
using TourPlanner.Models;


namespace TourPlanner.BLL
{
    public interface IAppManagerFactory
    {
        IEnumerable<TourItem> SearchForTours(string searchTourName, bool caseSensitive = false);
        bool SearchForLogs(TourItem tour, string searchLogReport, bool caseSensitive = false);
        IEnumerable<TourItem> GetTours();
        TourItem CreateTour(string name, string description, string from, string to);
        TourItem CreateTour(TourItem tour);
        TourItem DeleteTour(TourItem currentTour);
        TourItem UpdateTour(TourItem currentTour);
        IEnumerable<LogItem> GetLogs(TourItem tour);
        LogItem CreateLog(TourItem tour, DateTime datetime, string report, double distance, TimeSpan totaltime, double rating,
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories);
        LogItem DeleteLog(LogItem logItem);
        LogItem UpdateLog(LogItem logItem);
        TourItem ImportTour();
        void ExportTour(TourItem currentTour);
        void PrintTour(TourItem currentTour, List<LogItem> tourLogs);
        void PrintAll(List<LogItem> allLogs);

    }
}
