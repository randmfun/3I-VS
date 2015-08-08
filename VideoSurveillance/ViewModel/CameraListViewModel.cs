﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SCD_UVSS.ViewModel
{
    class CameraListViewModel
    {
        private ObservableCollection<Image>  _buttons = new ObservableCollection<Image>();


        public CameraListViewModel()
        {
            for (int i=0; i < 10; i++)
            {
                Image imageViewer = new Image();

                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(@"D:\personal\code\vs-jac\VideoSurveillance\Images\WIN_20141123_103530.JPG");
                logo.EndInit();
                
                imageViewer.Source = logo;
                imageViewer.Height = 128;
                imageViewer.Width = 128;
               
                _buttons.Add(imageViewer);
            }
        }

        public ObservableCollection<Image> Cameras
        {
            get { return _buttons; }
        }

        public void Update()
        {
            for (int i=0; i < 10; i++)
            {
                Image imageViewer = this.Cameras[i];

                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(@"D:\me.jpg");
                logo.EndInit();

                imageViewer.Source = logo;
            }
            
        }

        
    }
}