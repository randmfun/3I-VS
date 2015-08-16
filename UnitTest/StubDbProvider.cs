using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;

namespace UnitTest
{
    public class StubDbProvider : IDatabaseProvider
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

        public bool AddGateInfo()
        {
            throw new NotImplementedException();
        }
    }
}
