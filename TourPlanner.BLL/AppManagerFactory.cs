using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BLL
{
    public static class AppManagerFactory
    {
        private static IAppManager manager;

        public static IAppManager GetFactoryManager()
        {
            if(manager == null)
            {
                manager = new AppManagerImpl();
            }
            return manager;
        }
    }
}
