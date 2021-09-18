﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.BLL;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{

    public partial class AddTourWindow : Window
    {
        public AddTourWindow(MainViewModel mainWindow)
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(this, mainWindow);
        }
    }
}
