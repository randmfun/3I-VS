using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.DBProviders
{
    public class MySqlDatabaseProvider : IDatabaseProvider
    {
        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public bool Open()
        {
            throw new NotImplementedException();
        }

        public bool Close()
        {
            throw new NotImplementedException();
        }

        public bool AddBlackListItem(BlackListItem blackListItem)
        {
            throw new NotImplementedException();
        }

        public List<BlackListItem> GetAllBlackListItem()
        {
            var lst = new List<BlackListItem>();
            for(int i = 0; i <100; i++)
                lst.Add(new BlackListItem(string.Format("TN 07 {0}", i.ToString())));
            return lst;
        }

        public bool AddVehicleEntryBasicInfo(VehicleBasicInfoModel vehicleBasicInfoModel)
        {
            throw new NotImplementedException();
        }

        public bool AddVehicleEntryImges(VehicleImagesModel vehicleImagesModel)
        {
            throw new NotImplementedException();
        }

        public bool AddGateInfo(Gate gate)
        {
            throw new NotImplementedException();
        }

        public Gate ReadGateInfo()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DbSearchResultModel> Search(DbSearchRequestModel dbSearchRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
