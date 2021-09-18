using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using log4net;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;
using TourPlanner.Models;

namespace TourPlanner.DAL.PostgresSqlServer
{
    public class LogItemPostgresSqlServerDAO : ILogItemDAO
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"logitem\" WHERE \"id\" = @id;";
        private const string SQL_FIND_BY_TOUR_ID = "SELECT * FROM public.\"logitem\" WHERE \"tourid\" = @tourid;";
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"logitem\"" +
                                                  "(tourid, datetime, report, distance, totaltime, rating, averagespeed, maxspeed, minspeed, averagestepcount, burntcalories)" +
                                                  "VALUES(@tourid, @datetime, @report, @distance, @totalTime, @rating, @averagespeed,@maxspeed,@minspeed,@averagestepcount,@burntcalories) " +
                                                  "RETURNING \"id\";";
        private const string SQL_DELETE_ITEM = "DELETE FROM public.\"logitem\" WHERE \"id\" = @id;";
        private const string SQL_UPDATE_ITEM = "UPDATE public.\"logitem\" SET \"datetime\" = @datetime,\"report\" = @report," +
                                               "\"distance\" = @distance,\"totaltime\" = @totaltime,\"rating\" = @rating," +
                                               "\"averagespeed\" = @averagespeed,\"maxspeed\" = @maxspeed,\"minspeed\" = @minspeed, \"averagestepcount\" = @averagestepcount, " +
                                               "\"burntcalories\" = @burntcalories WHERE \"id\" = @id;";

        private IDatabase database;
        private DAO.ITourItemDAO touritemDAO;

        public LogItemPostgresSqlServerDAO()
        {
            this.database = DALFactory.GetDatabase();
            this.touritemDAO = DALFactory.CreateTourItemDAO();
        }

        public LogItem FindById(int id)
        {
            DbCommand findCommand = null;
            IEnumerable<LogItem> logsList = null;
            try
            {
                findCommand = database.CreateCommand(SQL_FIND_BY_ID);
                database.DefineParameter(findCommand, "@id", DbType.Int32, id);
                logsList = QueryLogItemsFromDatabase(findCommand);
                log.Info($"Log found by ID {id} in database.");

            }
            catch (Exception ex)
            {
                log.Error($"Log could not be found by ID {id} in database: {ex}");
            }
            return logsList.FirstOrDefault();
        }

        public LogItem AddNewItem(TourItem tour, DateTime datetime, string report, double distance, TimeSpan totaltime,
            double rating, double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories)
        {
            DbCommand insertCommand = null;
            try
            {
                insertCommand = database.CreateCommand(SQL_INSERT_NEW_LOG);
                database.DefineParameter(insertCommand, "@tourid", DbType.Int32, tour.Id);
                database.DefineParameter(insertCommand, "@datetime", DbType.DateTime, datetime);
                database.DefineParameter(insertCommand, "@totaltime", DbType.Time, totaltime);
                database.DefineParameter(insertCommand, "@report", DbType.String, report);
                database.DefineParameter(insertCommand, "@distance", DbType.Double, distance);
                database.DefineParameter(insertCommand, "@rating", DbType.Double, rating);
                database.DefineParameter(insertCommand, "@averagespeed", DbType.Double, averagespeed);
                database.DefineParameter(insertCommand, "@maxspeed", DbType.Double, maxspeed);
                database.DefineParameter(insertCommand, "@minspeed", DbType.Double, minspeed);
                database.DefineParameter(insertCommand, "@averagestepcount", DbType.Double, averagestepcount);
                database.DefineParameter(insertCommand, "@burntcalories", DbType.Double, burntcalories);
                log.Info($"Log successfully added for the tour {tour.Name} to database.");

            }
            catch (Exception ex)
            {

                log.Error($"Log could no be added for the tour {tour.Name} to database: {ex}");
            }

            return FindById(database.ExecuteScalar(insertCommand));
        }


        public LogItem DeleteItem(LogItem logitem)
        {
            DbCommand deleteCommand = null;
            try
            {
                deleteCommand = database.CreateCommand(SQL_DELETE_ITEM);
                database.DefineParameter(deleteCommand, "@id", DbType.Int32, logitem.Id);

                log.Info($"Log successfully deleted from database.");

            }
            catch (Exception ex)
            {
                log.Error($"Log could no be deleted from database: {ex}");
            }

            database.ExecuteScalar(deleteCommand);
            return logitem;
        }

        public LogItem UpdateItem(LogItem logitem)
        {

            DbCommand updateCommand = null;

            try
            {
                updateCommand = database.CreateCommand(SQL_UPDATE_ITEM);
                database.DefineParameter(updateCommand, "@id", DbType.Int32, logitem.Id);
                database.DefineParameter(updateCommand, "@datetime", DbType.DateTime, logitem.DateTime);
                database.DefineParameter(updateCommand, "@totaltime", DbType.Time, logitem.TotalTime);
                database.DefineParameter(updateCommand, "@report", DbType.String, logitem.Report);
                database.DefineParameter(updateCommand, "@distance", DbType.Double, logitem.Distance);
                database.DefineParameter(updateCommand, "@rating", DbType.Double, logitem.Rating);
                database.DefineParameter(updateCommand, "@averagespeed", DbType.Double, logitem.AverageSpeed);
                database.DefineParameter(updateCommand, "@maxspeed", DbType.Double, logitem.MaxSpeed);
                database.DefineParameter(updateCommand, "@minspeed", DbType.Double, logitem.MinSpeed);
                database.DefineParameter(updateCommand, "@averagestepcount", DbType.Double, logitem.AverageStepCount);
                database.DefineParameter(updateCommand, "@burntcalories", DbType.Double, logitem.BurntCalories);

                log.Info($"Log successfully deleted from database.");

            }
            catch (Exception ex)
            {
                log.Error($"Log could no be deleted from database: {ex}");
            }
            

            database.ExecuteScalar(updateCommand);
            return logitem;
        }

        public IEnumerable<LogItem> GetLogs(TourItem tour)
        {
            DbCommand getLogsCommand = null;
            try
            {
                getLogsCommand = database.CreateCommand(SQL_FIND_BY_TOUR_ID);
                database.DefineParameter(getLogsCommand, "@tourid", DbType.Int32, tour.Id);
                log.Info($"Succesfully retrieved logs for tour {tour.Name}");

            }
            catch (Exception ex)
            {
                log.Error($"Could not retrieve Logs for tour {tour.Id} from database: {ex}");
            }
            return QueryLogItemsFromDatabase(getLogsCommand);
        }

        private IEnumerable<LogItem> QueryLogItemsFromDatabase(DbCommand command)
        {
            List<LogItem> logsList = new List<LogItem>();
            try
            {
                using (IDataReader reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        logsList.Add(new LogItem(
                            (int)reader["id"],
                            touritemDAO.FindById((int)reader["tourid"]),
                            (DateTime)reader["datetime"],
                            (string)reader["report"],
                            (double)reader["distance"],
                            (TimeSpan)reader["totaltime"],
                            (double)reader["rating"],
                            (double)reader["averagespeed"],
                            (double)reader["maxspeed"],
                            (double)reader["minspeed"],
                            (double)reader["averagestepcount"],
                            (double)reader["burntcalories"])
                        );
                    }
                }
                log.Info($"Succesfully queried tour logs from database");

            }
            catch (Exception ex)
            {

                log.Error($"Could not query Logs from database : {ex}");
            }

            return logsList;
        }
    }
}
