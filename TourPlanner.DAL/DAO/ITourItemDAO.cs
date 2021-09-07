using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DAL.DAO
{
    public interface ITourItemDAO
    {
        TourItem FindById(Guid itemId);

        TourItem AddNewItem(string name, string tourdescription, string from, string to, string routeinformation,
            double tourdistance);

        TourItem AddNewItem(TourItem item); //overloading method to copy items

        IEnumerable<TourItem> GetTours();

    }
}
