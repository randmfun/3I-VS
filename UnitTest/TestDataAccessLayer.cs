using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;

namespace UnitTest
{
    [TestFixture]
    public class TestDataAccessLayer
    {

        [Test]
        public void TestAddBlackListItem()
        {
            Assert.AreEqual("", "");
        }

        [Test]
        public void GetAllBlackListItem()
        {
            
        }

        [Test]
        public void AddVehicleEntryBasicInfo()
        {
            
        }

        [Test]
        public void AddVehicleEntryImges()
        {
            
        }

        [Test]
        public void TestValidateGateInformationNoErrors()
        {
            var gateModel = new Gate("one");
            gateModel.Cameras.Add(new CameraModel() {SavePath = Path.GetTempPath()});

            var dataAccesslayer = new DataAccessLayer(new MockDatabaseProvider());
            var errors = dataAccesslayer.ValidateGateInformation(gateModel);

            Assert.IsFalse(errors.Length > 0);
        }

        [Test]
        public void TestValidateGateInformationNoSavePath()
        {
            var gateModel = new Gate("one");
            gateModel.Cameras.Add(new CameraModel());

            var dataAccesslayer = new DataAccessLayer(new MockDatabaseProvider());
            var hasError = dataAccesslayer.ValidateGateInformation(gateModel);

            Assert.IsTrue(hasError.Length > 0);
        }

        [Test]
        public void TestValidateGateInformationInvalidSavePath()
        {
            var gateModel = new Gate("one");
            gateModel.Cameras.Add(new CameraModel(){SavePath = Path.GetTempFileName()});

            var dataAccesslayer = new DataAccessLayer(new MockDatabaseProvider());
            var hasError = dataAccesslayer.ValidateGateInformation(gateModel);

            Assert.IsTrue(hasError.Length > 0);
        }

    }
}
