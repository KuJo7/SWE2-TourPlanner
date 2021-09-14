using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DAL.DAO
{
    public interface ITourItemDAO
    {
        TourItem FindById(int id);

        IEnumerable<TourItem> GetTours();

        TourItem AddNewItem(string name, string description, string from, string to, string routeinformation, int distance, string imagepath);

        TourItem DeleteItem(TourItem tour);

        TourItem UpdateItem(TourItem tour);


    }
}
