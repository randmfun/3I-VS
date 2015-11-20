using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using log4net;
using SCD_UVSS.Dal;
using SCD_UVSS.ImageProcessor;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace SCD_UVSS.Controller
{
    public class RecordManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        private readonly DataAccessLayer _dataAccessLayer;
        
        public readonly GateProvider GateProvider;

        public bool ContinueRecording { get; set; }

        public RecordManager(GateProvider gateProvider, DataAccessLayer dataAccessLayer)
        {
            this.GateProvider = gateProvider;
            this._dataAccessLayer = dataAccessLayer;
        }

        public delegate void VehicleInfomationHandler(VehicleBasicInfoModel infoModel, VehicleImagesModel imagesModel); 
        
        public event VehicleInfomationHandler VehicleInformationRecived;

        public bool StopRecording { get; set; }

        public void StartRecording()
        {
            Logger.Info("Started Recording...");

            while (this.ContinueRecording)
            {
                VehicleBasicInfoModel vehicleBasicInfoModel;
                VehicleImagesModel vehicleImagesModel;

                if (!this.StartRecordingInfo(out vehicleBasicInfoModel, out vehicleImagesModel)) continue;

                // We have a new Vehichle information, Notify the UI, to Referesh the content
                this.OnVehicleInformationRecived(vehicleBasicInfoModel, vehicleImagesModel);

                Thread.Sleep(1000);
            }
        }
        
        public bool StartRecordingInfo(out VehicleBasicInfoModel vehicleBasicInfoModel, out VehicleImagesModel vehicleImagesModel)
        {
            vehicleBasicInfoModel = null;
            vehicleImagesModel = null;

            try
            {
                if (!this.HasLoopStarted()) return false;

                // There is a NEW Vehicle entered, Start capturing
                this.RecordCurrentSnapshots(out vehicleBasicInfoModel, out vehicleImagesModel);

                // Done Capturing, signal Firmware
                this.EndLoop(vehicleBasicInfoModel.IsBlackListed);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("StartRecordingInfo Failed, Exception ", ex);
                return false;
            }
        }

        public void RecordCurrentSnapshots(out VehicleBasicInfoModel vehicleBasicInfoModel, out VehicleImagesModel vehicleImagesModel)
        {
            Logger.Info("RecordCurrentSnapshots Started..");

            vehicleBasicInfoModel = new VehicleBasicInfoModel
            {
                DateTime = DateTime.Now,
                UniqueEntryId = Guid.NewGuid().ToString()
            };

            //vehicleBasicInfoModel.Number = ""
            var vehicleNumber = this.GateProvider.LicenceNumberProvider.Read();

            vehicleBasicInfoModel.Number = vehicleNumber;
            vehicleBasicInfoModel.IsBlackListed = this.IsBlackListedNumber(vehicleNumber);

            vehicleImagesModel = new VehicleImagesModel(vehicleBasicInfoModel.UniqueEntryId);
            
            var chasisImages = new List<byte[]>();

            foreach (var cameraProvider in this.GateProvider.CameraProviders)
            {
                byte[] image = cameraProvider.Read();
                switch (cameraProvider.CameraModel.ImageType)
                {
                    case ImageType.ChaisisImage:
                        chasisImages.Add(image);
                        break;
                    case ImageType.DriverImage:
                        vehicleImagesModel.DriverImage = image;
                        break;
                    case ImageType.NumberPlateImage:
                        vehicleImagesModel.NumberPlateImage = image;
                        break;
                    case ImageType.VehicleOverallImage:
                        vehicleImagesModel.VehicleOverallImage = image;
                        break;
                }
            }

            vehicleImagesModel.ChaisisImage = ImageSticher.Sticher(chasisImages);
            
            Logger.Info("RecordCurrentSnapshots Ended..");
        }

        private bool HasLoopStarted()
        {
            try
            {
                Logger.Info("Check Loop Started..");
                var readLine = this.GateProvider.ComPortProvider.Read();
                return readLine.Trim() == "S";
            }
            catch (Exception ex)
            {
                Logger.Error("Checking Has loop started failed!!");
                Logger.Fatal(ex.Message);        
            }
            return false;
        }

        private void EndLoop(bool isBlacklisted)
        {
            try
            {
                Logger.Info("End Loop ..");
                const string blackListedMessage = "B";
                const string safelyEndMessage = "E";

                string endLoopMessage = isBlacklisted ? blackListedMessage : safelyEndMessage;
                this.GateProvider.ComPortProvider.Write(endLoopMessage);
            }
            catch (Exception ex)
            {
                Logger.Error("End Loop Crashed.");
                Logger.Fatal(ex.Message);
            }
        }

        public bool IsBlackListedNumber(string vehicleNumber)
        {
            var allBlackListedItems = this._dataAccessLayer.GetAllBlackListItem();
            return allBlackListedItems.Any(item => item.VehicleNumber == vehicleNumber);
        }

        protected void OnVehicleInformationRecived(VehicleBasicInfoModel vehicleBasicInfoModel, VehicleImagesModel vehicleImagesModel)
        {
            Logger.Info("Inside OnVehicleInformationRecived");

            if (VehicleInformationRecived != null)
            {
                Logger.Info("Firing OnVehicleInformationRecived");
                
                this.VehicleInformationRecived(vehicleBasicInfoModel, vehicleImagesModel);
                this._dataAccessLayer.AddVehicleEntryBasicInfo(vehicleBasicInfoModel);
                this._dataAccessLayer.AddVehicleEntryImges(vehicleImagesModel);
            }
        }
    }
}
