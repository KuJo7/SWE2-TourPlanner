using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TourPlanner.BLL.MapQuest;
using TourPlanner.Models;
using TourPlanner.DAL;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;
using Microsoft.Win32;
using QuestPDF.Fluent;
using TourPlanner.BLL.Reporting;

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
            IMapQuest mapQuest = new MapQuest.MapQuest();
            if (mapQuest.fetchData(from, to))
            {
                string imagepath = mapQuest.LoadImage();
                int distance = mapQuest.GetDistance();
                string routeinformation = "test";

                return tourItemDAO.AddNewItem(name, description, from, to, routeinformation, distance, imagepath);
            }
            else
            {

                return null;
            }
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

        public TourItem ImportTour()
        {

            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["FilePath"];

            openFileDialog.ShowDialog();

            var JsonData = File.ReadAllText(openFileDialog.FileName);
            var tour = JsonConvert.DeserializeObject<TourItem>(JsonData);
            return CreateTour(tour);
        }

        public void ExportTour(TourItem currentTour)
        {
            var fileId = Guid.NewGuid().ToString();
            string filePath = ConfigurationManager.AppSettings["FilePath"] + currentTour.Name + "_" + fileId + ".txt";
            var tour = JsonConvert.SerializeObject(currentTour);
            File.WriteAllText(filePath, tour);
        }

        public void PrintTour(TourItem currentTour, List<LogItem> tourLogs)
        {
            var fileId = Guid.NewGuid().ToString();
            string filePath = ConfigurationManager.AppSettings["TourReportPath"] + currentTour.Name + "_" + fileId + ".pdf";
            TourReport tourReport = new TourReport(currentTour, tourLogs);
            tourReport.GeneratePdf(filePath);
        }

        public void PrintAll(List<LogItem> allLogs)
        {
            string filePath = ConfigurationManager.AppSettings["SummaryReportPath"] + DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_") + ".pdf";
            SummaryReport summaryReport = new SummaryReport(allLogs);
            summaryReport.GeneratePdf(filePath);
        }
    }
}
