using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BLL.Factory
{
    public static class AppManagerFactory
    {
        private static IAppManagerFactory manager;

        public static IAppManagerFactory GetFactoryManager()
        {
            if(manager == null)
            {
                manager = new AppManagerFactoryImpl();
            }
            return manager;
        }
    }
}
