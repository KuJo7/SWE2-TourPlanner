using System;
using System.Configuration;
using System.Reflection;
using TourPlanner.DAL.DAO;

namespace TourPlanner.DAL.Common
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;
        private static IDatabase database;
        private static IFileAccess fileAccess;
        private static bool useFileSystem;


        static DALFactory()
        {
            useFileSystem = bool.Parse(ConfigurationManager.AppSettings["useFileSystem"]);
            if (useFileSystem)
            {
               assemblyName = ConfigurationManager.AppSettings["DALFileAssembly"];
            }
            else
            { 
                assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];

            }
            dalAssembly = Assembly.Load(assemblyName);
        }

        public static IDatabase GetDatabase()
        {
            if (database == null)
            {
                database = CreateDatabase();
            }

            return database;
        }
        
        private static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSqlConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        private static IDatabase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass, new object[] { connectionString }) as IDatabase;
        }

        public static ITourItemDAO CreateTourItemDAO()
        {
            string className = assemblyName + ".TourItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".TourItemFileServerDAO";
            }
            Type tourType = dalAssembly.GetType(className);

            return Activator.CreateInstance(tourType) as ITourItemDAO;
        }

        public static ITourItemDAO DeleteTourItemDAO()
        {
            string className = assemblyName + ".TourItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".TourItemFileServerDAO";
            }
            Type tourType = dalAssembly.GetType(className);

            return Activator.CreateInstance(tourType) as ITourItemDAO;
        }

        public static ITourItemDAO UpdateTourItemDAO()
        {
            string className = assemblyName + ".TourItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".TourItemFileServerDAO";
            }
            Type tourType = dalAssembly.GetType(className);

            return Activator.CreateInstance(tourType) as ITourItemDAO;
        }

        public static ILogItemDAO CreateTourLogDAO()
        {
            string className = assemblyName + ".LogItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".LogItemFileServerDAO";
            }
            Type logType = dalAssembly.GetType(className);

            return Activator.CreateInstance(logType) as ILogItemDAO;
        }

        public static ILogItemDAO CreateLogItemDAO()
        {
            string className = assemblyName + ".LogItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".LogItemFileServerDAO";
            }
            Type logType = dalAssembly.GetType(className);

            return Activator.CreateInstance(logType) as ILogItemDAO;
        }

        public static ILogItemDAO DeleteLogItemDAO()
        {
            string className = assemblyName + ".LogItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".LogItemFileServerDAO";
            }
            Type logType = dalAssembly.GetType(className);

            return Activator.CreateInstance(logType) as ILogItemDAO;
        }

        public static ILogItemDAO UpdateLogItemDAO()
        {
            string className = assemblyName + ".LogItemPostgresSqlServerDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".LogItemFileServerDAO";
            }
            Type logType = dalAssembly.GetType(className);

            return Activator.CreateInstance(logType) as ILogItemDAO;
        }

        public static IFileAccess GetFileAccess()
        {
            if (fileAccess == null)
            {
                fileAccess = CreateFileAccess();
            }

            return fileAccess;
        }

        private static IFileAccess CreateFileAccess()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StartFolderFilePath"].ConnectionString;
            return CreateFileAccess(connectionString);
        }

        private static IFileAccess CreateFileAccess(string connectionString)
        {
            string fileClassName = assemblyName + ".FileAccess";
            Type fileClass = dalAssembly.GetType(fileClassName);

            return Activator.CreateInstance(fileClass, new object[] { connectionString }) as IFileAccess;
        }
    }
}
