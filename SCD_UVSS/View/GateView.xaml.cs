﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.View
{
    /// <summary>
    /// Interaction logic for GateView.xaml
    /// </summary>
    public partial class GateView : UserControl
    {
        public GateSetupViewModel GateSetupViewModel { get; set; }
        
        public GateView()
        {
            InitializeComponent();

            this.GateSetupViewModel = new GateSetupViewModel(new DataAccessLayer(new MySqlDatabaseProvider()));

            this.DataContext = this.GateSetupViewModel;
        }
    }
}