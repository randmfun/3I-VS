using System;
using System.IO;
using System.Windows;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CameraListViewModel cameraListViewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            this.cameraListViewModel = new CameraListViewModel();
            this.CameraViews.DataContext = this.cameraListViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.cameraListViewModel.Update();

            var trip_id_guid = Guid.NewGuid();
            var trip_image_guid = Guid.NewGuid();
            /*
            var tripInfo = TripInfo.CreateTripInfo(trip_id_guid, DateTime.Now, "TNO7 5689",);
            var tripImage = TripImage.CreateTripImage(guid, GetImage(), GetImage(), GetImage(), GetImage());

            Entities entities = new Entities();

            entities.AddToTripInfoes(tripInfo);
            entities.SaveChanges();

            //entities.AddToTripImages(tripImage);
            //entities.SaveChanges();
             * */
        }

        byte[] GetImage()
        {
            var file_name = @"D:\me.jpg";
            byte[] data;

            using(FileStream fstrm=new FileStream(file_name, FileMode.Open))
            {
                data = new byte[fstrm.Length];
                fstrm.Read(data, 0, (int) fstrm.Length);
            }
            return data;
        }
    }
    
}
