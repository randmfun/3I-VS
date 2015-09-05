﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.DBProviders
{
    public class MockDatabaseProvider: IDatabaseProvider
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
            throw new NotImplementedException();
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
            var dbResult = new List<DbSearchResultModel>();

            for (int i = 211; i < 222; i++)
            {
                dbResult.Add(new DbSearchResultModel() { EntryDateTime = DateTime.Now, VehicleNumber = "TN 07 J 5746" });
            }

            return dbResult;
        }
    }
}