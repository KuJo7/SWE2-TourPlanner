using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.DAL;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;

namespace TourPlanner.BLL
{
    internal class AppManagerFactoryImpl : IAppManagerFactory
    {
        public IEnumerable<TourItem> SearchForTours(string searchTourName, bool caseSensitive = false)
        {
            IEnumerable<TourItem> tours = GetTours();

            if (caseSensitive)
            {
                return tours.Where(x => x.Name.Contains(searchTourName));
            }
            return tours.Where(x => x.Name.ToLower().Contains(searchTourName.ToLower()));
        }

        public bool SearchForLogs(TourItem tour, string searchLogReport, bool caseSensitive = false)
        {
            IEnumerable<LogItem> logs = GetLogs(tour);

            if (caseSensitive)
            {
                return logs.Any(x => x.Report.Contains(searchLogReport));
            }

            return logs.Any(x => x.Report.ToLower().Contains(searchLogReport.ToLower())); ;
        }
      
        public IEnumerable<TourItem> GetTours()
        {
            ITourItemDAO tourItemDAO = DALFactory.CreateTourItemDAO();
            return tourItemDAO.GetTours();
        }

        public TourItem CreateTour(string name, string description, string from, string to)
        {
            ITourItemDAO tourItemDAO = DALFactory.CreateTourItemDAO();
            //IMapQuest mapQuest = new MapQuest();
            string imagepath = "test";
                //mapQuest.LoadImage(from, to);
            string routeinformation = "test";
            int distance = 0;
            return tourItemDAO.AddNewItem(name, description, from, to, routeinformation, distance, imagepath);
        }

        public TourItem CreateTour(TourItem tour)
        {

            ITourItemDAO tourItemDAO = DALFactory.CreateTourItemDAO();
            return tourItemDAO.AddNewItem(tour.Name + "-Copy", tour.Description, tour.From, tour.To,
                tour.RouteInformation, tour.Distance, tour.ImagePath);
        }

        public TourItem DeleteTour(TourItem currentTour)
        {
            ITourItemDAO tourItemDAO = DALFactory.DeleteTourItemDAO();
            return tourItemDAO.DeleteItem(currentTour);
        }

        public TourItem UpdateTour(TourItem currentTour)
        {
            ITourItemDAO tourItemDAO = DALFactory.UpdateTourItemDAO();
            return tourItemDAO.UpdateItem(currentTour);
        }

        public IEnumerable<LogItem> GetLogs(TourItem tour)
        {
            ILogItemDAO logItemDAO = DALFactory.CreateTourLogDAO();
            return logItemDAO.GetLogs(tour);
        }

        public LogItem CreateLog(TourItem tour, DateTime datetime, string report, double distance, TimeSpan totaltime, double rating,
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories)
        {
            ILogItemDAO logItemDAO = DALFactory.CreateLogItemDAO();
            return logItemDAO.AddNewItem(tour, datetime, report, distance, totaltime, rating, averagespeed, maxspeed, minspeed, averagestepcount, burntcalories);
        }
        public LogItem DeleteLog(LogItem log)
        {
            ILogItemDAO logItemDAO = DALFactory.DeleteLogItemDAO();
            return logItemDAO.DeleteItem(log);
        }

        public LogItem UpdateLog(LogItem log)
        {
            ILogItemDAO logItemDAO = DALFactory.UpdateLogItemDAO();
            return logItemDAO.UpdateItem(log);
        }



    }
}
