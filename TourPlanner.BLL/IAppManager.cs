using System;
using System.Collections.Generic;
using TourPlanner.Models;


namespace TourPlanner.BLL
{
    public interface IAppManager
    {
        public IEnumerable<TourItem> GetTours();
        IEnumerable<TourItem> SearchForTours(string tourName, bool caseSensitive = false);
    }
}
