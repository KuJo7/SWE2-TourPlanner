using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.DAL;

namespace TourPlanner.BLL
{
    internal class AppManagerFactoryImpl : IAppManagerFactory
    {
        private readonly TourItemDAO _tourItemDAO = new TourItemDAO();

        public IEnumerable<TourItem> GetTours()
        {
            return _tourItemDAO.GetTours();
        }

        public TourItem UpdateTour(TourItem currentTour, string newName, string newRouteInformation, string newTourDescription, double newTourDistance)
        {
           return _tourItemDAO.UpdateTour();
        }
        
        public void DeleteTour(TourItem currentTour)
        {
          _tourItemDAO.DeleteTour();
        }

        public TourItem CopyTour(TourItem currentTour, string newName, string newRouteInformation, string newTourDescription)
        {
            TourItem copiedTour = currentTour;
            copiedTour.Name = newName;
            copiedTour.RouteInformation = newRouteInformation;
            copiedTour.TourDescription = newTourDescription;

            return AddTour(copiedTour);
        }
        public TourItem AddTour(TourItem tourItem)
        { 
           return _tourItemDAO.AddTour(tourItem);
        }

        public IEnumerable<TourItem> SearchForTours(string tourName, bool caseSensitive = false)
        {
            IEnumerable<TourItem> tours = GetTours();

            if (caseSensitive)
            {
                return tours.Where(x => x.Name.Contains(tourName));
            }
            return tours.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }
    }
}
