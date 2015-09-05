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
    }



    public class DbSearchResultModel
    {
        public DateTime EntryDateTime { get; set; }

        public string VehicleNumber { get; set; }

    }
    public class DbSearchRequestModel
    {
        public DateTime StaDateTime { get; set; }

        public DateTime EnDateTime { get; set; }

        public string VehicleNumber { get; set; }
    }
}
