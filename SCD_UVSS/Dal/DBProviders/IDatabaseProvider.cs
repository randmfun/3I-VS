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
    }
}
