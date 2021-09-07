using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class LogItem
    {
        private Guid Id { get; set; }
        private TourItem TourItem { get; set; }
        private DateTime DateTime { get; set; }
        private string Report { get; set; }
        private string Distance { get; set; }
        private TimeSpan TotalTime { get; set; }
        private double Rating { get; set; }
        private double AverageSpeed { get; set; }
        private double MaxSpeed { get; set; }
        private double MinSpeed { get; set; }
        private double AverageStepCount { get; set; }
        private double BurntCalories { get; set; }

        public LogItem(Guid id, TourItem touritem, DateTime datetime, string report, string distance, TimeSpan totaltime, double rating, 
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories)
        {
            this.Id = id;
            this.TourItem = touritem;
            this.DateTime = datetime;
            this.Report = report;
            this.Distance = distance;
            this.TotalTime = totaltime;
            this.Rating = rating;
            this.AverageSpeed = averagespeed;
            this.MaxSpeed = maxspeed;
            this.MinSpeed = minspeed;
            this.AverageStepCount = averagestepcount;
            this.BurntCalories = burntcalories;
        }
    }
}
