using System;
using System.Data;
using System.Data.Common;
using Npgsql;
using TourPlanner.DAL.Common;

namespace TourPlanner.DAL.PostgresSqlServer
{
    public class Database : IDatabase
    {
        private string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private DbConnection CreateConnection()
        {
            DbConnection con = new NpgsqlConnection(this.connectionString);
            con.Open();

            return con;
        }

        public DbCommand CreateCommand(string genericCommandText)
        {
            return new NpgsqlCommand(genericCommandText);
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            if (!command.Parameters.Contains(name))
            {
                var index = command.Parameters.Add(new NpgsqlParameter(name, type));
                return index;
            }
            else
            {
                throw new ArgumentException($"Parameter {name} already exists.");
            }
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            var index = DeclareParameter(command, name, type);
            command.Parameters[index].Value = value;
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            if (command.Parameters.Contains(name))
            {
                command.Parameters[name].Value = value;
            }
            else
            {
                throw new ArgumentException($"Parameter {name} does not exist.");
            }
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            command.Connection = CreateConnection();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteScalar(DbCommand command)
        {
            command.Connection = CreateConnection();
            return Convert.ToInt32(command.ExecuteScalar());
        }

    }
}
