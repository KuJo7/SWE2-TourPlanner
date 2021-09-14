using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;
using TourPlanner.Models;

namespace TourPlanner.DAL.PostgresSqlServer
{
    public class LogItemPostgresSqlServerDAO : ILogItemDAO
    {
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
                //log.Info("Tourlog " + id + "succesfully found by id in database");

            }
            catch (Exception ex)
            {
                //log.Error("Could not find tour log by id from database " + ex.Message);
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
                //log.Info("Succesfully added tour log into database for tour " + tourLogItem.Name + " with id " + tourLogItem.Id);

            }
            catch (Exception ex)
            {

                //log.Error("Could not add tour log to database " + ex.Message);
            }

            return FindById(database.ExecuteScalar(insertCommand));
        }


        public LogItem DeleteItem(LogItem log)
        {
            DbCommand deleteCommand = null;
            try
            {
                deleteCommand = database.CreateCommand(SQL_DELETE_ITEM);
                database.DefineParameter(deleteCommand, "@id", DbType.Int32, log.Id);

                //log.Info($"Tour {name} successfully deleted from database.");

            }
            catch (Exception ex)
            {
                //log.Error($"Tour {name} could no be deleted from database: {ex.Message}");
            }

            database.ExecuteScalar(deleteCommand);
            return log;
        }

        public LogItem UpdateItem(LogItem log)
        {

            DbCommand updateCommand = null;

            try
            {
                updateCommand = database.CreateCommand(SQL_UPDATE_ITEM);
                database.DefineParameter(updateCommand, "@id", DbType.Int32, log.Id);
                database.DefineParameter(updateCommand, "@datetime", DbType.DateTime, log.DateTime);
                database.DefineParameter(updateCommand, "@totaltime", DbType.Time, log.TotalTime);
                database.DefineParameter(updateCommand, "@report", DbType.String, log.Report);
                database.DefineParameter(updateCommand, "@distance", DbType.Double, log.Distance);
                database.DefineParameter(updateCommand, "@rating", DbType.Double, log.Rating);
                database.DefineParameter(updateCommand, "@averagespeed", DbType.Double, log.AverageSpeed);
                database.DefineParameter(updateCommand, "@maxspeed", DbType.Double, log.MaxSpeed);
                database.DefineParameter(updateCommand, "@minspeed", DbType.Double, log.MinSpeed);
                database.DefineParameter(updateCommand, "@averagestepcount", DbType.Double, log.AverageStepCount);
                database.DefineParameter(updateCommand, "@burntcalories", DbType.Double, log.BurntCalories);

                //log.Info($"Tour {name} successfully deleted from database.");

            }
            catch (Exception ex)
            {
                //log.Error($"Tour {name} could no be deleted from database: {ex.Message}");
            }
            

            database.ExecuteScalar(updateCommand);
            return log;
        }

        public IEnumerable<LogItem> GetLogs(TourItem tour)
        {
            DbCommand getLogsCommand = null;
            try
            {
                getLogsCommand = database.CreateCommand(SQL_FIND_BY_TOUR_ID);
                database.DefineParameter(getLogsCommand, "@tourid", DbType.Int32, tour.Id);
                //log.Info("Succesfully retrieved logs for tour " + tourItem.Name + " with id " + tourItem.Id);

            }
            catch (Exception ex)
            {
                //log.Error("Could not retrieve Logs for tour " + tourItem.Id + " from database " + ex.Message);
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
                //log.Info("Succesfully queried tour logs from database");

            }
            catch (Exception ex)
            {

                //log.Error("Could not query Logs from database " + ex.Message);
            }

            return logsList;
        }
    }
}
