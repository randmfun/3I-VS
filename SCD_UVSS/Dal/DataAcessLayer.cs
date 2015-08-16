using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal
{
    public class DataAccessLayer
    {
        private readonly IDatabaseProvider _databaseProvider;

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

    }
}
