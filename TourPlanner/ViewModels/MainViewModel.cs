using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TourPlanner.BLL;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IAppManager tourManager;

        public MainViewModel(IAppManager tourManager)
        {
            this.tourManager = tourManager;
            Debug.Print("Constructor MainViewModel");
            
        }

    }
}
