namespace TourPlanner.Models
{
    public class TourItem
    {
        private int _Id { get; set; }
        private string _Name { get; set; }
        private string _Description { get; set; }
        private string _From { get; set; }
        private string _To { get; set; }
        private string _RouteInformation { get; set; }
        private int _Distance { get; set; }
        private string _ImagePath { get; set; }

        public TourItem(int id, string name, string description, string from, string to, string routeinformation, int distance, string imagepath)
        {
            this._Id = id;
            this._Name = name;
            this._Description = description;
            this._From = from;
            this._To = to;
            this._RouteInformation = routeinformation;
            this._Distance = distance;
            this._ImagePath = imagepath;
        }

        public int Id
        {
            get => _Id;
            set => _Id = value;
        }
        public string Name
        {
            get => _Name;
            set => _Name = value;
        }
        public string Description
        {
            get => _Description;
            set => _Description = value;
        }
        public string From
        {
            get => _From;
            set => _From = value;
        }
        public string To
        {
            get => _To;
            set => _To = value;
        }
        public string RouteInformation
        {
            get => _RouteInformation;
            set => _RouteInformation = value;
        }
        public int Distance
        {
            get => _Distance;
            set => _Distance = value;
        }
        public string ImagePath
        {
            get => _ImagePath;
            set => _ImagePath = value;
        }


    }
}
