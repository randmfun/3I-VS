using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SCD_UVSS.Dal;
using SCD_UVSS.ImageProcessor;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;

namespace SCD_UVSS.Controller
{
    public class MockRecordManager: RecordManager
    {
        public MockRecordManager(GateProvider gateProvider, DataAccessLayer dataAccessLayer)
            : base(gateProvider, dataAccessLayer)
        {
            
        }

        static int newvehiclenumber = 1234;

        public new void StartRecording()
        {
            var vehicleBasicInfoModel = new VehicleBasicInfoModel
            {
                DateTime = DateTime.Now,
                UniqueEntryId = Guid.NewGuid().ToString()
            };

            //vehicleBasicInfoModel.Number = ""
            var number = newvehiclenumber++;

            vehicleBasicInfoModel.Number = number.ToString();
            vehicleBasicInfoModel.IsBlackListed = base.IsBlackListedNumber(number.ToString());

            var vehicleImagesModel = new VehicleImagesModel(vehicleBasicInfoModel.UniqueEntryId);

            var resourceDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            vehicleImagesModel.ChaisisImage = File.ReadAllBytes(Path.Combine(resourceDir, "chasis.jpg"));
            vehicleImagesModel.DriverImage = File.ReadAllBytes(Path.Combine(resourceDir, "driver.jpg"));
            vehicleImagesModel.NumberPlateImage = File.ReadAllBytes(Path.Combine(resourceDir, "driver.jpg")); 
            vehicleImagesModel.VehicleOverallImage = File.ReadAllBytes(Path.Combine(resourceDir, "car-topview.jpg"));
            
            base.OnVehicleInformationRecived(vehicleBasicInfoModel, vehicleImagesModel);
        }

    }
}
