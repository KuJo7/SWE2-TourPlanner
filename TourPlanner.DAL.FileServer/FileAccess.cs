using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TourPlanner.DAL.Common;
using TourPlanner.Models;

namespace TourPlanner.DAL.FileServer
{
    public class FileAccess : IFileAccess
    {
        private string filePath;

        public FileAccess(string filePath)
        {
            this.filePath = filePath;
        }

        private IEnumerable<FileInfo> GetFileInfos(string startFolder, ItemType searchType)
        {
            DirectoryInfo dir = new DirectoryInfo(startFolder);
            return dir.GetFiles("*" + searchType.ToString() + ".txt", SearchOption.AllDirectories);
        }

        private string GetFullPath(string fileName)
        {
            return Path.GetFullPath(fileName);
        }

        public int CreateNewTourFile(string name, string description, string from, string to, string routeinformation, int distance, string imagepath)
        {
            int id = Guid.NewGuid().GetHashCode();
            string fileName = id + "_TourItem.txt";
            string path = GetFullPath(fileName);
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine(id);
                writer.WriteLine(name);
                writer.WriteLine(description);
                writer.WriteLine(from);
                writer.WriteLine(to);
                writer.WriteLine(routeinformation);
                writer.WriteLine(distance);
                writer.WriteLine(imagepath);

            }
            return id;
        }

        public int CreateNewLogFile(TourItem tour, DateTime datetime, string report, double distance, TimeSpan totaltime,
            double rating, double averagespeed, double maxspeed, double minspeed, double averagestepcount,
            double burntcalories)
        {
            int id = Guid.NewGuid().GetHashCode();
            string fileName = id + "_LogItem.txt";
            string path = GetFullPath(fileName);
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine(id);
                writer.WriteLine(tour.Id);
                writer.WriteLine(datetime);
                writer.WriteLine(report);
                writer.WriteLine(distance);
                writer.WriteLine(totaltime);
                writer.WriteLine(rating);
                writer.WriteLine(averagespeed);
                writer.WriteLine(rating);
                writer.WriteLine(maxspeed);
                writer.WriteLine(minspeed);
                writer.WriteLine(averagestepcount);
                writer.WriteLine(burntcalories);

            }
            return id;
        }

        public IEnumerable<FileInfo> SearchFiles(string searchTerm, ItemType searchType)
        {
            IEnumerable<FileInfo> fileList = GetFileInfos(filePath, searchType);
            IEnumerable<FileInfo> queryMatchingFiles =
                from file in fileList
                let fileText = GetFileText(file)
                where fileText.Contains(searchTerm)
                select file;
            return queryMatchingFiles;
        }

        private string GetFileText(FileInfo file)
        {
            using StreamReader reader = file.OpenText();
            StringBuilder sb = new StringBuilder();
            while (!reader.EndOfStream)
            {
                sb.Append(reader.ReadLine());
            }
            return sb.ToString();
        }

        public IEnumerable<FileInfo> GetAllFiles(ItemType searchType)
        {
            return GetFileInfos(filePath, searchType);
        }

        public string CreateImage(string from, string to, string routeinformation)
        {
            throw new NotImplementedException();
        }
    }
}
