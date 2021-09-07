using System;
using System.Collections.Generic;
using TourPlanner.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL.DAO
{
    public interface ILogItemDAO
    {
        TourItem FindById(Guid itemId);

        LogItem AddNewItem(TourItem tourItem, DateTime datetime, string report, string distance, TimeSpan totaltime, double rating, 
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories); //parameter TourItem tourItem or Guid tourItemId
        
        IEnumerable<LogItem> GetLogs(TourItem tourItem);

    }
}
