using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;
using TourPlanner.Models;

namespace TourPlanner.DAL.FileServer
{
    class LogItemFileServerDAO : ILogItemDAO
    {
        private readonly IFileAccess fileAccess;
        private readonly ITourItemDAO tourItemDAO;

        public LogItemFileServerDAO()
        {
            this.fileAccess = DALFactory.GetFileAccess();
            this.tourItemDAO = DALFactory.CreateTourItemDAO();
        }

        public LogItem FindById(int id)
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(id.ToString(), ItemType.LogItem);
            return QueryLogItemFromFileSystem(foundFiles).FirstOrDefault();
        }

        public IEnumerable<LogItem> GetLogs(TourItem tour)
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(tour.Id.ToString(), ItemType.LogItem);
            return QueryLogItemFromFileSystem(foundFiles);
        }

        public LogItem AddNewItem(TourItem tour, DateTime datetime, string report, double distance, TimeSpan totaltime, double rating,
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories)
        {
            int id = fileAccess.CreateNewLogFile(tour, datetime, report, distance, totaltime, rating, averagespeed, maxspeed, minspeed, averagestepcount, burntcalories);
            return FindById(id);
        }

        public LogItem DeleteItem(LogItem log)
        {
            throw new NotImplementedException();
        }

        public LogItem UpdateItem(LogItem log)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<LogItem> QueryLogItemFromFileSystem(IEnumerable<FileInfo> foundFiles)
        {
            List<LogItem> foundTourLogs = new List<LogItem>();
            try
            {
                foreach (FileInfo file in foundFiles)
                {
                    string[] fileLines = File.ReadAllLines(file.FullName);
                    foundTourLogs.Add(new LogItem(
                        int.Parse(fileLines[0]),
                        tourItemDAO.FindById(int.Parse(fileLines[1])),
                        DateTime.Parse(fileLines[2]),
                        fileLines[3],
                        double.Parse(fileLines[4]),
                        TimeSpan.Parse(fileLines[5]),
                        double.Parse(fileLines[6]),
                        double.Parse(fileLines[7]),
                        double.Parse(fileLines[8]),
                        double.Parse(fileLines[9]),
                        double.Parse(fileLines[10]),
                        double.Parse(fileLines[11])
                    ));
                }
                //log.Info("Succesfully queried tour logs from filesystem");

            }
            catch (Exception ex)
            {

                //log.Error("Could not query tour logs from file system from tour " + ex.Message);
            }

            return foundTourLogs;
        }
    }
}
