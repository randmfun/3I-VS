using System;
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
using System.Windows.Shapes;
using SCD_UVSS.Dal;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.View
{
    /// <summary>
    /// Interaction logic for CameraImagesViewWindow.xaml
    /// </summary>
    public partial class CameraImagesViewWindow : Window
    {
        public CameraImagesViewWindow(DataAccessLayer dataAccessLayer, string uniqueId, string vehicleNumber)
        {
            InitializeComponent();

            var mainCameraView = new MainCameraView(dataAccessLayer)
            {
                DataContext = this.ConstructMainCameraViewModel(dataAccessLayer, uniqueId, vehicleNumber)
            };

            this.mainCameraViewControl.Content = mainCameraView;
        }

        private MainCameraViewModel ConstructMainCameraViewModel(DataAccessLayer dataAccessLayer, string uniqueId,
            string vehicleNumber)
        {
            var dbImageResult = dataAccessLayer.GetImageResult(uniqueId);
            return new MainCameraViewModel(vehicleNumber, dbImageResult.ChasisImage, dbImageResult.CarFullImage, dbImageResult.DriverImage);
        }
    }
}
