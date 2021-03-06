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
    /// Interaction logic for BlackListView.xaml
    /// </summary>
    public partial class BlackListView : UserControl
    {
        public BlackListView(DataAccessLayer dataAccessLayer)
        {
            InitializeComponent();

            this.DataContext = new BlackListViewModel(dataAccessLayer);
        }
    }
}
