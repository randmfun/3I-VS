using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.helpers;
using SCD_UVSS.Model;

namespace UnitTest
{
    [TestFixture]
    [Ignore("Ignore a MySqlLocalTest")]
    public class TestMySqlDatabaseProvider
    {
        const string ValidConnectionString = "SERVER=localhost;DATABASE=scduvss;UID=root;PASSWORD=Welcome@123;";

        [Test]
        public void TestOpenConnectionValid()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };
            Assert.IsTrue(mySqlProvider.Open(), "Test MySql Connection Failed");
        }

        [Test]
        public void TestOpenConnectionInValid()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = "SERVER=localhost;DATABASE=scduvss;UID=root;PASSWORD=123;"
            };
            Assert.False(mySqlProvider.Open(), "Test MySql Connection Failed");
        }
        
        [Test]
        public void TestCloseConnection()
        {
            var mySqlProvider = new MySqlDatabaseProvider();
            Assert.IsFalse(mySqlProvider.Close());
        }

        [Test]
        public void TestAddBlackListItem()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            Assert.IsTrue(mySqlProvider.AddBlackListItem(new BlackListItem("TN 01 1235")));
        }

        [Test]
        public void TestGetBlackListItem()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            var blackListItems = mySqlProvider.GetAllBlackListItem();

            Assert.IsTrue(blackListItems.Count > 2);
        }
        
        [Test]
        public void TestAddVehicleEntryBasicInfo()
        {
            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            var vehicleBasicInfoModel = new VehicleBasicInfoModel
            {
                UniqueEntryId = Guid.NewGuid().ToString(),
                Number = "TN 07 1234",
                DateTime = DateTime.Now
            };

            Assert.IsTrue(mySqlProvider.AddVehicleEntryBasicInfo(vehicleBasicInfoModel));
        }

        [Test]
        public void TestAddVehicleEntryImges()
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileName = Path.Combine(executiongDir, "Resources", "split_1.jpg");

            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            var vehicleImages = new VehicleImagesModel("a8e21388-a12e-4cb3-a920-35301ab593cf")
            {
                ChaisisImage = File.ReadAllBytes(fileName),
                DriverImage = File.ReadAllBytes(fileName),
                VehicleOverallImage = File.ReadAllBytes(fileName)
            };

            Assert.IsTrue(mySqlProvider.AddVehicleEntryImges(vehicleImages));
        }

        [Test]
        public void TestSearchVehicleData()
        {
            /*
               SELECT *
               FROM scduvss.vehicle_entry_info
               WHERE number = "TN 07 1234"
               AND entrytime >= '2015-10-01' AND entrytime < '2015-11-01'
               AND DATE_FORMAT(entrytime, '%H:%i:s') >= '10:00:00' AND DATE_FORMAT(entrytime, '%H:%i:s') > '22:00:00' 
            */

            var dbSearchRequestModel = new DbSearchRequestModel
            {
                StaDateTime = new DateTime(2015, 10, 01, 10, 00, 00),
                EnDateTime = new DateTime(2015, 11, 01, 22, 00, 00),
                VehicleNumber = "TN 07 1234"
            };

            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            var searchResult = mySqlProvider.Search(dbSearchRequestModel);

            Assert.IsTrue(searchResult.Any());
        }

        [Test]
        public void TestGetImageResult()
        {
            var id = "a8e21388-a12e-4cb3-a920-35301ab593cf";

            var mySqlProvider = new MySqlDatabaseProvider
            {
                ConnectionString = ValidConnectionString
            };

            var resutlt = mySqlProvider.GetImageResult(id);

            Assert.IsTrue(resutlt.ChasisImage != null);
        }

    }
}
