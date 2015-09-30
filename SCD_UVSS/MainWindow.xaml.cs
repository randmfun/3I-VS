using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using log4net;
using SCD_UVSS.Controller;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainWindow));
        
        public static bool _windowStateFullScreen = true;

        private MainTabViewModel _mainTabViewModel;

        public MainWindow()
        {
            try
            {
                logger.Info("Application Starting!!");

                InitializeComponent();
                this._mainTabViewModel = new MainTabViewModel();
                this.mainTabCtrl.DataContext = this._mainTabViewModel;

                logger.Info("Application Started!!");
                
            }
            catch (Exception ex)
            {
                logger.Error("Application Crashed.");
                logger.Fatal(ex);
            }
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


        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (_windowStateFullScreen)
                {
                    this.AppWindow.WindowState = WindowState.Normal;
                    this.AppWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
                }
                else
                {
                    this.AppWindow.WindowState = WindowState.Maximized;
                    this.AppWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                }
                _windowStateFullScreen = !_windowStateFullScreen;
            }
        }
    }
    
}
