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
    public class TourItemPostgresSqlServerDAO : ITourItemDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"touritem\" WHERE \"id\" = @id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"touritem\";";
        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"touritem\"" +
                                                   "(name, description, \"from\", \"to\", \"routeinformation\", distance, imagepath) " +
                                                   "VALUES(@name, @description, @from, @to, @routeinformation, @distance, @imagepath) " +
                                                   "RETURNING \"id\";";
        private const string SQL_DELETE_ITEM = "DELETE FROM public.\"touritem\" WHERE \"id\" = @id;";
        private const string SQL_UPDATE_ITEM = "UPDATE public.\"touritem\" SET \"name\" = @name,\"description\" = @description," +
                                               "\"from\" = @from,\"to\" = @to,\"routeinformation\" = @routeinformation," +
                                               "\"distance\" = @distance,\"imagepath\" = @imagepath WHERE \"id\" = @id;";

        
        
        private readonly IDatabase database;

        public TourItemPostgresSqlServerDAO()
        {
            this.database = DALFactory.GetDatabase();
        }

        public TourItem FindById(int id)
        {
            DbCommand findCommand = null;
            IEnumerable<TourItem> toursList = null;
            try
            {
                findCommand = database.CreateCommand(SQL_FIND_BY_ID);
                database.DefineParameter(findCommand, "@id", DbType.Int32, id);
                toursList = QueryTourItemsFromDatabase(findCommand);
                //log.Info($"Tour found by ID {itemId} in database.");

            }
            catch (Exception ex)
            {
                //log.Error("Tour could not be found by ID {itemId} in database: {ex.Message}");
            }
            return toursList.FirstOrDefault();
        }

        public TourItem AddNewItem(string name, string description, string from, string to, string routeinformation, int distance, string imagepath)
        {
            DbCommand insertCommand = null;
            try
            {
                insertCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
                database.DefineParameter(insertCommand, "@name", DbType.String, name);
                database.DefineParameter(insertCommand, "@description", DbType.String, description);
                database.DefineParameter(insertCommand, "@from", DbType.String, from);
                database.DefineParameter(insertCommand, "@to", DbType.String, to);
                database.DefineParameter(insertCommand, "@routeinformation", DbType.String, routeinformation);
                database.DefineParameter(insertCommand, "@distance", DbType.Int32, distance);
                database.DefineParameter(insertCommand, "@imagepath", DbType.String, imagepath);
                //log.Info($"Tour {name} successfully added to database.");

            }
            catch (Exception ex)
            {
                //log.Error($"Tour {name} could no be added to database: {ex.Message}");
            }

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public TourItem DeleteItem(TourItem tour)
        {
            DbCommand deleteCommand = null;
            try
            {
                deleteCommand = database.CreateCommand(SQL_DELETE_ITEM);
                database.DefineParameter(deleteCommand, "@id", DbType.Int32, tour.Id);

                //log.Info($"Tour {name} successfully deleted from database.");

            }
            catch (Exception ex)
            {
                //log.Error($"Tour {name} could no be deleted from database: {ex.Message}");
            }

            database.ExecuteScalar(deleteCommand);
            return tour;
        }

        public TourItem UpdateItem(TourItem tour)
        {
            DbCommand updateCommand = null;
            try
            {
                updateCommand = database.CreateCommand(SQL_UPDATE_ITEM);
                database.DefineParameter(updateCommand, "@id", DbType.Int32, tour.Id);
                database.DefineParameter(updateCommand, "@name", DbType.String, tour.Name);
                database.DefineParameter(updateCommand, "@description", DbType.String, tour.Description);
                database.DefineParameter(updateCommand, "@from", DbType.String, tour.From);
                database.DefineParameter(updateCommand, "@to", DbType.String, tour.To);
                database.DefineParameter(updateCommand, "@routeinformation", DbType.String, tour.RouteInformation);
                database.DefineParameter(updateCommand, "@distance", DbType.Int32, tour.Distance);
                database.DefineParameter(updateCommand, "@imagepath", DbType.String, tour.ImagePath);

                //log.Info($"Tour {name} successfully deleted from database.");

            }
            catch (Exception ex)
            {
                //log.Error($"Tour {name} could no be deleted from database: {ex.Message}");
            }

            database.ExecuteScalar(updateCommand);
            return tour;
        }


        public IEnumerable<TourItem> GetTours()
        {
            DbCommand touritemsCommand = database.CreateCommand(SQL_GET_ALL_ITEMS);
            return QueryTourItemsFromDatabase(touritemsCommand);
        }

        private IEnumerable<TourItem> QueryTourItemsFromDatabase(DbCommand command)
        {
            List<TourItem> toursList = new List<TourItem>();
            try
            {
                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        toursList.Add(new TourItem(
                            (int)reader["id"],
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["from"],
                            (string)reader["to"],
                            (string)reader["routeinformation"],
                            (int)reader["distance"],
                            (string)reader["imagepath"]
                        ));
                    }
                }
                //log.Info("Tours fetched successfully from database");
            }
            catch (Exception ex)
            {
                //log.Error("Tours could not be fetched from database: {ex.Message}");
            }

            return toursList;
        }
    }
}
