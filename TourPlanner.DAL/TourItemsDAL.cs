using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class TourItemsDAL
    {
        private string dbConnection;

        public TourItemsDAL(string connectionString)
        {
            // establish connection to DB
            dbConnection = connectionString;
        }

        public IEnumerable<TourItem> GetTours()
        {
            return new List<TourItem>() {
                new TourItem() { Name = "Tour1" },
                new TourItem() { Name = "Tour2" },
                new TourItem() { Name = "Tour3" },
                new TourItem() { Name = "Tour4" },
                new TourItem() { Name = "Tour5" }
            };
        }
    }
}
