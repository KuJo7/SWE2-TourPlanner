using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.DAL;

namespace TourPlanner.BLL
{
    internal class AppManagerImpl : IAppManager
    {
        public IEnumerable<TourItem> GetTours()
        {
            TourItemsDAL tourItemsDal = new TourItemsDAL("ConnectionString");
            return tourItemsDal.GetTours();
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
