using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DAL.DAO
{
    public interface ILogItemDAO
    {
        LogItem FindById(int id);

        IEnumerable<LogItem> GetLogs(TourItem tour);

        LogItem AddNewItem(TourItem tour, DateTime datetime, string report, double distance, TimeSpan totaltime, double rating,
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories);

        LogItem DeleteItem(LogItem log);

        LogItem UpdateItem(LogItem log);


    }
}
