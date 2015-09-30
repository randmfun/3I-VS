using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;
using SCD_UVSS.Annotations;
using SCD_UVSS.Dal;
using SCD_UVSS.helpers;

namespace SCD_UVSS.ViewModel
{
    public class MainCameraViewModel : INotifyPropertyChanged
    {
        private readonly DataAccessLayer _dataAccessLayer;

        private BitmapImage _chasisImage;
        private BitmapImage _carTopViewImage;
        private BitmapImage _driverImage;
        private string _vehicleNumber;

        public MainCameraViewModel(DataAccessLayer dataAccessLayer)
        {
            this._dataAccessLayer = dataAccessLayer;

            this._vehicleNumber = "TN 07 1234";
            this._chasisImage = this.GetDefaultImage("chasis.jpg");
            this._carTopViewImage = this.GetDefaultImage("car-topview.jpg");
            this._driverImage = this.GetDefaultImage("driver.jpg");

            this.StartRecording();
        }

        public BitmapImage ChasisImage
        {
            get { return this._chasisImage; }
            set
            {
                this._chasisImage = value;
                this.OnPropertyChanged("ChasisImage");
            }
        }

        public BitmapImage CarTopViewImage
        {
            get { return this._carTopViewImage; }
            set
            {
                this._carTopViewImage = value;
                this.OnPropertyChanged("CarTopViewImage");
            }
        }

        public BitmapImage DriverImage
        {
            get { return this._driverImage; }
            set
            {
                this._driverImage = value;
                this.OnPropertyChanged("DriverImage");
            }
        }

        public string VehicleNumber
        {
            get { return this._vehicleNumber; }
            set
            {
                this._vehicleNumber = value;
                this.OnPropertyChanged("VehicleNumber");
            }
        }
        
        private BitmapImage GetDefaultImage(string name)
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return ImageUtils.FiletoBitmapImage(Path.Combine(executiongDir, name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartRecording()
        {
            
        }
    }
}
