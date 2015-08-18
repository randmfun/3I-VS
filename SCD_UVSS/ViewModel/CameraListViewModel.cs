using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SCD_UVSS.ViewModel
{
    public class CameraListViewModel
    {
        private ObservableCollection<Image>  _buttons = new ObservableCollection<Image>();

        public CameraListViewModel()
        {
            for (int i=0; i < 10; i++)
            {
                Image imageViewer = new Image();

                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(@"D:\me.jpg");
                logo.EndInit();
                
                imageViewer.Source = logo;
                imageViewer.Height = 428;
                imageViewer.Width = 428;
               
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
