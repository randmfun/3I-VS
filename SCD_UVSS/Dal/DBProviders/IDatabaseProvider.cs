using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.DBProviders
{
    public interface IDatabaseProvider
    {
        bool CreateDatabase();

        bool Open();

        bool Close();

        bool AddBlackListItem(BlackListItem blackListItem);
        
        List<BlackListItem> GetAllBlackListItem();

        bool AddVehicleEntryBasicInfo(VehicleBasicInfoModel vehicleBasicInfoModel);

        bool AddVehicleEntryImges(VehicleImagesModel vehicleImagesModel);

        bool AddGateInfo(Gate gate);

        Gate ReadGateInfo();

        IEnumerable<DbSearchResultModel> Search(DbSearchRequestModel dbSearchRequestModel);

        DbImageResult GetImageResult(string uniqueId);
    }
    
    public class DbSearchResultModel
    {
        public DbSearchResultModel(DateTime entDateTime, string vehicleNumber, string uniqueId)
        {
            this.EntryDateTime = entDateTime;
            this.VehicleNumber = vehicleNumber;
            this.UniqueId = uniqueId;
        }

        public DbSearchResultModel() { }

        public DateTime EntryDateTime { get; set; }

        public string VehicleNumber { get; set; }

        public string UniqueId { get; set; }
    }

    public class DbSearchRequestModel
    {
        public DateTime StaDateTime { get; set; }

        public DateTime EnDateTime { get; set; }

        public string VehicleNumber { get; set; }
    }

    public class DbImageResult
    {
        public byte[] ChasisImage { get; set; }
        public byte[] DriverImage { get; set; }
        public byte[] CarFullImage { get; set; }
    }

}
