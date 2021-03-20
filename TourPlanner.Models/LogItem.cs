using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class LogItem
    {
        public DateTime DateTime { get; set; }
        public string Report { get; set; }
        public string Distance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public float Rating { get; set; }
    }
}
