using System;
using System.Collections.Generic;
using System.IO;
using TourPlanner.Models;

namespace TourPlanner.DAL.Common
{
    public interface IFileAccess
    {
        int CreateNewTourFile(string name, string description, string from, string to, string routeInformation, int distance);
        int CreateNewLogFile(TourItem tourItem, DateTime datetime, string report, string distance, TimeSpan totaltime, double rating, 
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories);

        IEnumerable<FileInfo> SearchFiles(string searchTerm, ItemType searchType);

        IEnumerable<FileInfo> GetAllFiles(ItemType searchType);

        public string CreateImage(string from, string to, string routeInformation);
    }
}
