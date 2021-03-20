using System;
using System.Collections.Generic;
using TourPlanner.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
{
    public class LogItemsDAL
    {
        private string dbConnection;

        public LogItemsDAL(string connectionString)
        {
            // establish connection to DB
            dbConnection = connectionString;
        }

        public IEnumerable<LogItem> GetLogs(string tourname)
        {
            return new List<LogItem>() {

            };
        }
    }
}
