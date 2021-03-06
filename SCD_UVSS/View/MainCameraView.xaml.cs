﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.View
{
    /// <summary>
    /// Interaction logic for MainCameraView.xaml
    /// </summary>
    public partial class MainCameraView : UserControl
    {
        private MainCameraViewModel _mainCameraViewModel;

        public MainCameraView(DataAccessLayer dataAccessLayer)
        {
            InitializeComponent();

            this._mainCameraViewModel = new MainCameraViewModel(dataAccessLayer);
            this._mainCameraViewModel.ShowMessage += _mainCameraViewModel_ShowMessage;
            this.DataContext = this._mainCameraViewModel;
        }

        // For Search View
        public MainCameraView(string vehicleNumber, byte[] chasisImage, byte[] carTopImage, byte[] driverImage)
        {
            InitializeComponent();

            this._mainCameraViewModel = new MainCameraViewModel(vehicleNumber, chasisImage, carTopImage, driverImage);
            this.DataContext = this._mainCameraViewModel;
        }

        void _mainCameraViewModel_ShowMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }
    }
}
