using System;
using System.Collections.Generic;
using TourPlanner.Models;


namespace TourPlanner.BLL
{
    public interface IAppManagerFactory
    {
        public IEnumerable<TourItem> GetTours();
        public void DeleteTour(TourItem currentTour);
        public TourItem UpdateTour(TourItem currentTour, string newName, string newRouteInformation, string newTourDescription, double newTourDistance);
        public TourItem CopyTour(TourItem currentTour, string newName, string newRouteInformation, string newTourDescription);
        public TourItem AddTour(TourItem tourItem);
        IEnumerable<TourItem> SearchForTours(string tourName, bool caseSensitive = false);
    }
}
