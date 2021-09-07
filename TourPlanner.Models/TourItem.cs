using System;

namespace TourPlanner.Models
{
    public class TourItem
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string TourDescription { get; set; }
        private string From { get; set; }
        private string To { get; set; }
        private string RouteInformation { get; set; }
        private double TourDistance { get; set; }

        public TourItem(Guid id, string name, string tourdescription, string from, string to, string routeinformation, double tourdistance)
        {
            this.Id = id;
            this.Name = name;
            this.TourDescription = tourdescription;
            this.From = from;
            this.To = to;
            this.RouteInformation = routeinformation;
            this.TourDistance = tourdistance;

        }
    }
}
