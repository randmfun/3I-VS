using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using log4net;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace SCD_UVSS.Controller
{
    public class RecordManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainWindow));
        
        public readonly GateProvider GateProvider;

        public bool ContinueRecording { get; set; }

        public RecordManager(GateProvider gateProvider)
        {
            this.GateProvider = gateProvider;
        }
        
        public event EventHandler VehicleImagesReceived;

        public bool StopRecording { get; set; }

        public void StartRecording()
        {
            while (this.ContinueRecording)
            {
                if (this.HasLoopStarted())
                {
                    
                }
            }
        }

        public void RecordCurrentSnapshots(out VehicleBasicInfoModel vehicleBasicInfoModel, out VehicleImagesModel vehicleImagesModel)
        {
            vehicleBasicInfoModel = new VehicleBasicInfoModel
            {
                DateTime = DateTime.Now,
                UniqueEntryId = Guid.NewGuid().ToString()
            };

            //vehicleBasicInfoModel.Number = ""
            //vehicleBasicInfoModel.IsBlackListed = false;

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
                this.GateProvider.ComPortProvider.Read();
            }
            catch (Exception ex)
            {
                logger.Error("Checking Has look started failed.");
                logger.Fatal(ex.Message);        
            }
            return true;
        }

        private void EndLoop()
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error("End Loop Crashed.");
                logger.Fatal(ex.Message);
            }
            
        }
    }


}
