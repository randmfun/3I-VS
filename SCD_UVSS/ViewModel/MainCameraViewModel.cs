using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using log4net;
using SCD_UVSS.Annotations;
using SCD_UVSS.Controller;
using SCD_UVSS.Dal;
using SCD_UVSS.helpers;
using SCD_UVSS.ImageProcessor;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;

namespace SCD_UVSS.ViewModel
{
    public class MainCameraViewModel : INotifyPropertyChanged
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        private readonly DataAccessLayer _dataAccessLayer;
        private Thread _thread;

        private BitmapImage _chasisImage;
        private BitmapImage _carTopViewImage;
        private BitmapImage _driverImage;
        private BitmapImage _licencePlateImage;

        private string _vehicleNumber;
        
        private bool _isBlackListed;

        private readonly RecordManager _recordManager = null;

        public event ErrorMessageDelegate ShowMessage;

        public ICommand StartRecordingCmd { get; set; }

        public ICommand StopRecordingCmd { get; set; }

        public string startRecordButtonContent = "START";
        public string stopRecordButtonContent = "STOP";

        private UserInfo _loggedInUserInfo;

        public UserInfo LoggedInUser
        {
            get { return _loggedInUserInfo ?? (_loggedInUserInfo = UserManager.Instance.LoggedInUser); }
        }

        public string StopRecordButtonContent
        {
            get { return stopRecordButtonContent; }
            set
            {
                this.stopRecordButtonContent = value;
                OnPropertyChanged("StopRecordButtonContent");
            }
        }

        public string StartRecordButtonContent
        {
            get { return startRecordButtonContent; }
            set
            {
                startRecordButtonContent = value;
                OnPropertyChanged("StartRecordButtonContent");
            }
        }

        public MainCameraViewModel(DataAccessLayer dataAccessLayer)
        {
            this._dataAccessLayer = dataAccessLayer;
            this.StartRecordingCmd = new RelayCommand(StartRecordingHandler);
            this.StopRecordingCmd = new RelayCommand(StopRecordingHandler);

            this._vehicleNumber = "TN 00 0000";
            this._chasisImage = this.GetDefaultImage("no-chasis.jpg");
            this._carTopViewImage = this.GetDefaultImage("no-car-topview.jpg");
            this._driverImage = this.GetDefaultImage("no-driver.jpg");
            this._licencePlateImage = this.GetDefaultImage("no-licence.jpg");

            var gateProvider = new GateProvider(this._dataAccessLayer.ReadGateInfo());
            this._recordManager = new RecordManager(gateProvider, dataAccessLayer);

            this._thread = new Thread(RecordDelegate);
        }

        // For Search View
        public MainCameraViewModel(string vehicleNumber, byte[] chasisImage, byte[] carTopImage, byte[] driverImage)
        {
            this._vehicleNumber = vehicleNumber;

            var chasis = ImageUtils.ByteArrayToBitMapImage(chasisImage);
            if (chasis != null)
                this._chasisImage = chasis;

            var topview = ImageUtils.ByteArrayToBitMapImage(carTopImage);
            if (topview != null)
                this._carTopViewImage = topview;

            var driverImg = ImageUtils.ByteArrayToBitMapImage(driverImage);
            if (driverImage != null)
                this._driverImage = driverImg;
        }

        public void StartRecordingHandler(object obj)
        {
            this.StartRecordingThread();
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

        public BitmapImage LicencePlateImage
        {
            get { return this._licencePlateImage; }
            set
            {
                this._licencePlateImage = value;
                this.OnPropertyChanged("LicencePlateImage");
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

        public bool IsBlackListed
        {
            get { return this._isBlackListed; }
            set
            {
                this._isBlackListed = value;
                this.OnPropertyChanged("IsBlackListed");
            }
        }

        public bool IsStartStopBtnVisible
        {
            get
            {
                return LoggedInUser.Role == Roles.Developer;
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
        
        public void StartRecordingThread()
        {
            this._recordManager.ContinueRecording = true;
            Logger.Info("Start Recording Handler - Start Recording");

            if (!this._thread.IsAlive)
            {
                this.StartRecordButtonContent = "Recording..";

                this._thread.IsBackground = true;
                this._thread.SetApartmentState(ApartmentState.STA);
                this._thread.Start();

                Logger.Info("Started Recording Thread");
            }
        }

        public void StopRecordingHandler(object obj)
        {
            this._recordManager.ContinueRecording = false;
            this.StartRecordButtonContent = "START";
            Logger.Info("Stop Recording Handler - Stopped Recording");
        }

        private void RecordDelegate()
        {
            this._recordManager.VehicleInformationRecived += RecordManagerVehicleInformationRecived;
            this._recordManager.StartRecording();
        }

        private void RecordManagerVehicleInformationRecived(Model.VehicleBasicInfoModel infoModel,
            Model.VehicleImagesModel imagesModel)
        {
            try
            {
                Logger.Info("Inside RecordManagerVehicleInformationRecived");

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    this.VehicleNumber = infoModel.Number;
                    this._isBlackListed = infoModel.IsBlackListed;

                    this.ChasisImage = ImageUtils.ByteArrayToBitMapImage(imagesModel.ChaisisImage);
                    this.DriverImage = ImageUtils.ByteArrayToBitMapImage(imagesModel.DriverImage);
                    this.CarTopViewImage = ImageUtils.ByteArrayToBitMapImage(imagesModel.VehicleOverallImage);
                    this.LicencePlateImage = ImageUtils.ByteArrayToBitMapImage(imagesModel.NumberPlateImage);
                }
                    ));

                Logger.Info("Done Dispatch: RecordManagerVehicleInformationRecived");
            }
            catch (Exception exception)
            {
                Logger.Error("Done Dispatch: RecordManagerVehicleInformationRecived", exception);
                if (this.ShowMessage != null)
                    this.ShowMessage(exception.Message);
                Logger.Fatal(exception.Message, exception.InnerException);
            }
        }

        public void TestSticher()
        {
            // Arrange
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var tempFileName = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetTempFileName()), ".jpg");

            var fileList = new List<string> { "split_1.jpg", "split_2.jpg" };
            var absFileList = from fileName in fileList
                              select Path.Combine(executiongDir, "Resources", fileName);

            // Action
            ImageSticher.Sticher(absFileList, tempFileName);

         
        }
    }
}
