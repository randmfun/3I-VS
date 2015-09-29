using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using log4net;
using SCD_UVSS.Dal;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace SCD_UVSS.Controller
{
    public class RecordManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        public readonly DataAccessLayer dataAccessLayer;
        
        public readonly GateProvider GateProvider;

        public bool ContinueRecording { get; set; }

        public RecordManager(GateProvider gateProvider, DataAccessLayer dataAccessLayer)
        {
            this.GateProvider = gateProvider;
            this.dataAccessLayer = dataAccessLayer;
        }

        public delegate void VehicleInfomationHandler(VehicleBasicInfoModel infoModel, VehicleImagesModel imagesModel); 
        
        public event VehicleInfomationHandler VehicleInformationRecived;

        public bool StopRecording { get; set; }

        public void StartRecording()
        {
            while (this.ContinueRecording)
            {
                VehicleBasicInfoModel vehicleBasicInfoModel;
                VehicleImagesModel vehicleImagesModel;

                if (!this.StartRecordingInfo(out vehicleBasicInfoModel, out vehicleImagesModel)) continue;

                if (VehicleInformationRecived != null)
                    this.VehicleInformationRecived(vehicleBasicInfoModel, vehicleImagesModel);
            }
        }

        public bool StartRecordingInfo(out VehicleBasicInfoModel vehicleBasicInfoModel, out VehicleImagesModel vehicleImagesModel)
        {
            vehicleBasicInfoModel = null;
            vehicleImagesModel = null;

            if (!this.HasLoopStarted()) return false;

            this.RecordCurrentSnapshots(out vehicleBasicInfoModel, out  vehicleImagesModel);
            this.EndLoop();
            return true;
        }
        
        public void RecordCurrentSnapshots(out VehicleBasicInfoModel vehicleBasicInfoModel, out VehicleImagesModel vehicleImagesModel)
        {
            vehicleBasicInfoModel = new VehicleBasicInfoModel
            {
                DateTime = DateTime.Now,
                UniqueEntryId = Guid.NewGuid().ToString()
            };

            //vehicleBasicInfoModel.Number = ""
            var number = "";
            vehicleBasicInfoModel.IsBlackListed = this.IsBlackListedNumber(number);

            vehicleImagesModel = new VehicleImagesModel(vehicleBasicInfoModel.UniqueEntryId);
            
            foreach (var cameraProvider in this.GateProvider.CameraProviders)
            {
                byte[] image = cameraProvider.Read();
                switch (cameraProvider.CameraModel.ImageType)
                {
                    case ImageType.ChaisisImage:
                        vehicleImagesModel.ChaisisImage = image;
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
            return true;
        }

        private void EndLoop()
        {
            try
            {
                Logger.Info("End Loop ..");
                const string endLoopMessage = "E";
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
            var allBlackListedItems = this.dataAccessLayer.GetAllBlackListItem();
            return allBlackListedItems.Any(item => item.VehicleNumber == vehicleNumber);
        }
    }
    
}
