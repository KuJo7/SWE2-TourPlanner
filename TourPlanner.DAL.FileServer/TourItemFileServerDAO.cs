using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TourPlanner.DAL.Common;
using TourPlanner.DAL.DAO;
using TourPlanner.Models;

namespace TourPlanner.DAL.FileServer
{
    class TourItemFileServerDAO : ITourItemDAO
    {
        private IFileAccess fileAccess;

        public TourItemFileServerDAO()
        {
            this.fileAccess = DALFactory.GetFileAccess();
        }

        public TourItem FindById(int id)
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(id.ToString(), ItemType.TourItem);
            return QueryTourItemFromFileSystem(foundFiles).FirstOrDefault();
        }

        public IEnumerable<TourItem> GetTours()
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.GetAllFiles(ItemType.TourItem);
            return QueryTourItemFromFileSystem(foundFiles);
        }

        public TourItem AddNewItem(string name, string description, string from, string to, string routeinformation, int distance,
            string imagepath)
        {
            int id = fileAccess.CreateNewTourFile(name, description, from, to, routeinformation, distance, imagepath);
            return FindById(id);
        }

        public TourItem DeleteItem(TourItem tour)
        {
            throw new NotImplementedException();
        }

        public TourItem UpdateItem(TourItem tour)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<TourItem> QueryTourItemFromFileSystem(IEnumerable<FileInfo> foundFiles)
        {
            List<TourItem> foundTourItems = new List<TourItem>();
            try
            {
                foreach (FileInfo file in foundFiles)
                {
                    string[] fileLines = File.ReadAllLines(file.FullName);
                    foundTourItems.Add(new TourItem(
                        int.Parse(fileLines[0]),
                        fileLines[1],
                        fileLines[2],
                        fileLines[3],
                        fileLines[4],
                        fileLines[5],
                        int.Parse(fileLines[6]),
                        fileLines[7]
                    ));

                }
                //log.Info("Succesfully queried tours from filesystem");

            }
            catch (Exception ex)
            {

                //log.Error("Could not query tours from file system " + ex.Message);
            }

            return foundTourItems;
        }
    }
}
