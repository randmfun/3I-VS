using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.helpers;
using SCD_UVSS.Model;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.Dal
{
    public class DataAccessLayer
    {
        private readonly IDatabaseProvider _databaseProvider;
        public string GateInfoFileName = "gateinfo.bin";

        public DataAccessLayer(IDatabaseProvider databaseProvider)
        {
            this._databaseProvider = databaseProvider;
        }

        public void AddBlackListItem(BlackListItem blackListItem)
        {
            this._databaseProvider.AddBlackListItem(blackListItem);
        }

        public List<BlackListItem> GetAllBlackListItem()
        {
            return this._databaseProvider.GetAllBlackListItem();
        }

        public bool AddVehicleEntryBasicInfo(VehicleBasicInfoModel vehicleBasicInfoModel)
        {
            return this._databaseProvider.AddVehicleEntryBasicInfo(vehicleBasicInfoModel);
        }

        public bool AddVehicleEntryImges(VehicleImagesModel vehicleImagesModel)
        {
            return this._databaseProvider.AddVehicleEntryImges(vehicleImagesModel);
        }

        public bool AddGateInfo(Gate gate)
        {
            return this._databaseProvider.AddGateInfo(gate);
        }

        public bool SaveGateInfo(Gate gate)
        {
            return SerializeUtility.Save(gate, GateInfoFileName);
        }

        public Gate ReadGateInfo()
        {
            return File.Exists(GateInfoFileName) ? SerializeUtility.Read<Gate>(GateInfoFileName) : this.ConstructDummyGateSetupItems();
        }

        public IEnumerable<DbSearchResultModel> Search(DbSearchRequestModel dbSearchRequestModel)
        {
            return this._databaseProvider.Search(dbSearchRequestModel);
        }

        public DbImageResult GetImageResult(string uniqueId)
        {
            return this._databaseProvider.GetImageResult(uniqueId);
        }

        public StringBuilder ValidateGateInformation(Gate gate)
        {
            var errorMsg = new StringBuilder();

            foreach (var camera in gate.Cameras)
            {
                if (string.IsNullOrEmpty(camera.SavePath))
                    errorMsg.AppendLine(string.Format("For Camera {0}, Save Path {1} is Empty", camera.Name, camera.SavePath));
                else if(!Directory.Exists(camera.SavePath))
                    errorMsg.AppendLine(string.Format("{0} : Save Path {1} Doesnt exists", camera.Name, camera.SavePath));
            }
            return errorMsg;
        }

        private Gate ConstructDummyGateSetupItems()
        {
            var gate = new Gate("Main Entry Gate");
            gate.Cameras.Add(new CameraModel()
            {
                Name = "Chasis One", ImageType = ImageType.ChaisisImage 
            });
            gate.Cameras.Add(new CameraModel() { Name = "Chasis Two", ImageType = ImageType.ChaisisImage });
            gate.Cameras.Add(new CameraModel() { Name = "Top View", ImageType = ImageType.VehicleOverallImage });
            gate.Cameras.Add(new CameraModel() { Name = "Driver Image", ImageType = ImageType.DriverImage });
            gate.Cameras.Add(new CameraModel() { Name = "Licence Plate", ImageType = ImageType.NumberPlateImage });
            gate.ComPortName = "COM2";
            gate.VehicleNumberSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return gate;
        }
    }

}
