using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BLL.MapQuest
{
    public interface IMapQuest
    {
        public bool fetchData(string from, string to);
        public string LoadImage();
        public bool DoesLocationExist(string location);
        public int GetDistance();
    }
}
