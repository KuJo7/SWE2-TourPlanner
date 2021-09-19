using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using TourPlanner.BLL.Factory;
using TourPlanner.BLL.MapQuest;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;
using TourPlanner.DAL.PostgresSqlServer;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class Tests
    {
        [Test]
        public void CreateTourItemModelId()
        {
            var tourItem = new TourItem(1, "Name1", "Description1", "From", "To", "RI", 10, "Path");
            Assert.AreEqual(1,tourItem.Id);
        }

        [Test]
        public void CreateTourItemModelName()
        {
            var tourItem = new TourItem(2, "Name2", "Description2", "From", "To", "RI", 10, "Path");
            Assert.AreEqual("Name2", tourItem.Name);
        }

        [Test]
        public void CreateTourItemModelDescription()
        {
            var tourItem = new TourItem(3, "Name3", "Description3", "From", "To", "RI", 10, "Path");
            Assert.AreEqual("Description3", tourItem.Description);
        }

        [Test]
        public void CreateLogItemModelId()
        {
            var tour = new TourItem(1, "Test1", "Test1", "Test1", "Test1", "Test1", 0, "Test1");
            var date = new DateTime(2021, 09, 20);
            var time = new TimeSpan(5, 00, 00);
            var logItem = new LogItem(1, tour, date, "Report", 10, time, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(1, logItem.Id);
        }

        [Test]
        public void CreateLogItemModelDate()
        {
            var tour = new TourItem(2, "Test2", "Test2", "Test2", "Test2", "Test2", 0, "Test2");
            var date = new DateTime(2021, 09, 20);
            var time = new TimeSpan(5, 00, 00);
            var logItem = new LogItem(2, tour, date, "Report", 10, time, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(date, logItem.DateTime);
        }

        [Test]
        public void CreateLogItemModelReport()
        {
            var tour = new TourItem(1, "Test3", "Test3", "Test3", "Test3", "Test3", 0, "Test3");
            var date = new DateTime(2021, 09, 20);
            var time = new TimeSpan(5, 00, 00);
            var logItem = new LogItem(3, tour, date, "Report", 10, time, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual("Report", logItem.Report);
        }

        [Test]
        public void MapQuestDoesLocationExist()
        {
            var mapQuest = new MapQuest();
            var result = mapQuest.DoesLocationExist("Wien");
            Assert.IsTrue(result);
        }

        [Test]
        public void MapQuestFetchData()
        {
            var mapQuest = new MapQuest();
            var result = mapQuest.fetchData("Wien", "Graz");
            Assert.IsTrue(result);
        }

        [Test, Order(1)]
        public void PostgresAddNewTourItem1()
        {
            var postgres = new TourItemPostgresSqlServerDAO();
            var result = postgres.AddNewItem("Test3", "Test3", "Test3", "Test3", "Test3", 0, "Test3");
            Assert.IsNotNull(result);
        }

        [Test, Order(2)]
        public void SearchForTour1()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var result = _appManagerFactory.SearchForTours("test3", false).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [Test, Order(3)]
        public void SearchForTour2()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var result = _appManagerFactory.SearchForTours("Test3", true).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [Test, Order(4)]
        public void PostgresAddNewTourItem2()
        {
            var postgres = new TourItemPostgresSqlServerDAO();
            var result = postgres.AddNewItem("Test4", "Test4", "Test4", "Test4", "Test4", 0, "Test4");
            Assert.IsNotNull(result);
        }

        [Test, Order(5)]
        public void PostgresAddNewLogItem1()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            tours = tours.Where(t => t.Name == "Test3").ToList();
            Assert.AreEqual(1, tours.Count());
            var date = new DateTime(2021, 09, 20);
            var time = new TimeSpan(3, 00, 00);
            var postgres = new LogItemPostgresSqlServerDAO();
            postgres.AddNewItem(tours[0], date, "Report3", 3, time, 3, 3, 3, 3 ,3 , 3);
            var result = postgres.GetLogs(tours[0]).ToList();

            Assert.AreEqual(1, result.Count());
        }

        [Test, Order(6)]
        public void PostgresAddNewLogItem2()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            tours = tours.Where(t => t.Name == "Test4").ToList();
            Assert.AreEqual(1, tours.Count());
            var date = new DateTime(2021, 09, 20);
            var time = new TimeSpan(4, 00, 00);
            var postgres = new LogItemPostgresSqlServerDAO();
            postgres.AddNewItem(tours[0], date, "Report4", 4, time, 4, 4, 4, 4, 4, 4);
            var result = postgres.GetLogs(tours[0]).ToList();

            Assert.AreEqual(1, result.Count());
        }

        [Test, Order(7)]
        public void SearchForTour3()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var result = _appManagerFactory.SearchForTours("test4", false).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [Test, Order(8)]
        public void SearchForTour4()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var result = _appManagerFactory.SearchForTours("Test4", true).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [Test, Order(9)]
        public void ClearTours()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var result = _appManagerFactory.SearchForTours("", true).ToList();
            var tours = _appManagerFactory.GetTours().ToList();
            Assert.AreEqual(tours.Count, result.Count);
        }

        [Test, Order(10)]
        public void PostgresUpdateTourItem1()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            var tour = tours.First();

            var newTour = new TourItem(tour.Id, "NEW3", "Test3", "Test3", "Test3", "Test3", 0, "Test3");
            var result = _appManagerFactory.UpdateTour(newTour);

            tours = _appManagerFactory.GetTours().ToList();
            tours = tours.Where(t => t.Name == "NEW3").ToList();
            Assert.AreEqual(1, tours.Count());
            Assert.AreEqual("NEW3", tours[0].Name);
        }

        [Test, Order(11)]
        public void PostgresUpdateTourItem2()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();

            var newTour = new TourItem(tours[0].Id, "NEW4", "Test4", "Test4", "Test4", "Test4", 0, "Test4");
            var result = _appManagerFactory.UpdateTour(newTour);

            tours = _appManagerFactory.GetTours().ToList();
            tours = tours.Where(t => t.Name == "NEW4").ToList();
            Assert.AreEqual(1, tours.Count());
            Assert.AreEqual("NEW4", tours[0].Name);
        }

        [Test, Order(12)]
        public void PostgresCopyTourItem1()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            var tour = tours.First();


            var result = _appManagerFactory.CreateTour(tour);
            Assert.AreEqual(tour.Name + "-Copy", result.Name);
        }

        [Test, Order(13)]
        public void PostgresCopyTourItem2()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();

            var result = _appManagerFactory.CreateTour(tours[1]);
            Assert.AreEqual(tours[1].Name + "-Copy", result.Name);
        }



        [Test, Order(14)]
        public void PostgresDeleteAllLogs()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            var result = 0;
            foreach (var tour in tours)
            {
                var logs = _appManagerFactory.GetLogs(tour).ToList();
                foreach (var log in logs)
                {
                    var postgres = new LogItemPostgresSqlServerDAO();
                    postgres.DeleteItem(log);
                }

                result = logs.Count();
            }
            Assert.AreEqual(0, result);
        }

        [Test, Order(15)]
        public void PostgresDeleteTourItem1()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            tours = tours.Where(t => t.Name == "NEW3").ToList();
            Assert.AreEqual(1, tours.Count());
            var postgres = new TourItemPostgresSqlServerDAO();
            var result = postgres.DeleteItem(tours[0]);
            Assert.IsNotNull(result);
        }

        [Test, Order(16)]
        public void PostgresDeleteTourItem2()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            tours = tours.Where(t => t.Name == "NEW4").ToList();
            Assert.AreEqual(1, tours.Count());
            var postgres = new TourItemPostgresSqlServerDAO();
            var result = postgres.DeleteItem(tours[0]);
            Assert.IsNotNull(result);
        }

        [Test, Order(17)]
        public void PostgresDeleteAllTours()
        {
            IAppManagerFactory _appManagerFactory = AppManagerFactory.GetFactoryManager();
            var tours = _appManagerFactory.GetTours().ToList();
            foreach (var item in tours)
            {
                var postgres = new TourItemPostgresSqlServerDAO();
                var result = postgres.DeleteItem(item);
            }
            tours = _appManagerFactory.GetTours().ToList();
            Assert.AreEqual(0, tours.Count());
        }

    }
}