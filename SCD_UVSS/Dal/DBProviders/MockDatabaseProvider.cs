using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.DBProviders
{
    public class MockDatabaseProvider: IDatabaseProvider
    {
        public List<BlackListItem> BlackListItems = new List<BlackListItem>(); 

        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public bool Open()
        {
            return true;
        }

        public bool Close()
        {
            return true;
        }

        public bool AddBlackListItem(BlackListItem blackListItem)
        {
            this.BlackListItems.Add(blackListItem);
            return true;
        }

        public List<BlackListItem> GetAllBlackListItem()
        {
            return this.BlackListItems;
        }

        public bool AddVehicleEntryBasicInfo(VehicleBasicInfoModel vehicleBasicInfoModel)
        {
            return true;
        }

        public bool AddVehicleEntryImges(VehicleImagesModel vehicleImagesModel)
        {
            return true;
        }

        public bool AddGateInfo(Gate gate)
        {
            return true;
        }

        public Gate ReadGateInfo()
        {
            return new Gate("");
        }

        public IEnumerable<DbSearchResultModel> Search(DbSearchRequestModel dbSearchRequestModel)
        {
            var dbResult = new List<DbSearchResultModel>();

            for (int i = 211; i < 222; i++)
            {
                dbResult.Add(new DbSearchResultModel() { EntryDateTime = DateTime.Now, VehicleNumber = "TN 07 J 1234" });
            }

            return dbResult;
        }

        public DbImageResult GetImageResult(string uniqueId)
        {
            throw new NotImplementedException();
        }
    }
}
