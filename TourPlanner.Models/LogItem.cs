using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class LogItem
    {
        private int _Id { get; set; }
        private TourItem _TourItem { get; set; }
        private DateTime _DateTime { get; set; }
        private string _Report { get; set; }
        private double _Distance { get; set; }
        private TimeSpan _TotalTime { get; set; }
        private double _Rating { get; set; }
        private double _AverageSpeed { get; set; }
        private double _MaxSpeed { get; set; }
        private double _MinSpeed { get; set; }
        private double _AverageStepCount { get; set; }
        private double _BurntCalories { get; set; }

        public LogItem(int id, TourItem touritem, DateTime datetime, string report, double distance, TimeSpan totaltime, double rating,
            double averagespeed, double maxspeed, double minspeed, double averagestepcount, double burntcalories)
        {
            this._Id = id;
            this._TourItem = touritem;
            this._DateTime = datetime;
            this._Report = report;
            this._Distance = distance;
            this._TotalTime = totaltime;
            this._Rating = rating;
            this._AverageSpeed = averagespeed;
            this._MaxSpeed = maxspeed;
            this._MinSpeed = minspeed;
            this._AverageStepCount = averagestepcount;
            this._BurntCalories = burntcalories;
        }

        public int Id
        {
            get => _Id;
            set => _Id = value;
        }

        public TourItem TourItem
        {
            get => _TourItem;
            set => _TourItem = value;
        }

        public DateTime DateTime
        {
            get => _DateTime;
            set => _DateTime = value;
        }

        public string Report
        {
            get => _Report;
            set => _Report = value;
        }

        public double Distance
        {
            get => _Distance;
            set => _Distance = value;
        }

        public TimeSpan TotalTime
        {
            get => _TotalTime;
            set => _TotalTime = value;
        }

        public double Rating
        {
            get => _Rating;
            set => _Rating = value;
        }

        public double AverageSpeed
        {
            get => _AverageSpeed;
            set => _AverageSpeed = value;
        }

        public double MaxSpeed
        {
            get => _MaxSpeed;
            set => _MaxSpeed = value;
        }

        public double MinSpeed
        {
            get => _MinSpeed;
            set => _MinSpeed = value;
        }

        public double AverageStepCount
        {
            get => _AverageStepCount;
            set => _AverageStepCount = value;
        }

        public double BurntCalories
        {
            get => _BurntCalories;
            set => _BurntCalories = value;
        }
    }
}
