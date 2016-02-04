using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Controller;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace UnitTest
{
    public class TestRecordManger
    {
        private static Gate ConstructGate()
        {
            UserManager.Instance.SetLoggedInUser("super");

            var gate = new Gate("Main Entry Gate");

            gate.Cameras.Add(new CameraModel() { Name = "Chasis One", ImageType = ImageType.ChaisisImage });
            gate.Cameras.Add(new CameraModel() { Name = "Chasis Two", ImageType = ImageType.ChaisisImage });
            gate.Cameras.Add(new CameraModel() { Name = "Top View", ImageType = ImageType.VehicleOverallImage });
            gate.Cameras.Add(new CameraModel() { Name = "Driver Image", ImageType = ImageType.DriverImage });
            gate.Cameras.Add(new CameraModel() { Name = "Licence Plate", ImageType = ImageType.NumberPlateImage });
            gate.ComPortName = "COM2";

            return gate;
        }

        private static DataAccessLayer ConstructMockDataAccessLayer()
        {
            var mockDatabaseProvider = new MockDatabaseProvider();
            var list = new[] { "TN 00 0000", "TN 00 1234" };
            mockDatabaseProvider.BlackListItems = (from s in list
                                                   select new BlackListItem(s)).ToList();

            var dataAccessLayer = new DataAccessLayer(mockDatabaseProvider);
            return dataAccessLayer;
        }

        [Test]
        public void TestRecordManagerConstruct()
        {
            var gateProvider = new GateProvider(ConstructGate());

            var recordManager = new RecordManager(gateProvider, ConstructMockDataAccessLayer());

            Assert.AreEqual(recordManager.ContinueRecording, false);
        }

        [Test]
        public void TestRecordCurrentSnapshots()
        {
            var gate = ConstructGate();

            var byteAry = new byte[] {1, 2, 3};
            var cameraProviders = gate.Cameras.Select(x => new MockCameraProvider(x, byteAry));

            var gateProvider = new GateProvider(gate) {CameraProviders = cameraProviders};
            
            var recordManager = new RecordManager(gateProvider,ConstructMockDataAccessLayer());

            VehicleBasicInfoModel vehicleBasicInfoModel;
            VehicleImagesModel vehicleImagesModel;

            recordManager.RecordCurrentSnapshots(out vehicleBasicInfoModel, out vehicleImagesModel);
            
            Assert.NotNull(vehicleBasicInfoModel.DateTime);
            Assert.NotNull(vehicleBasicInfoModel.UniqueEntryId);
            Assert.AreEqual(vehicleBasicInfoModel.UniqueEntryId, vehicleImagesModel.ForeignKeyId);
            Assert.AreEqual(vehicleBasicInfoModel.IsBlackListed, false);
        }

        [Test]
        public void TestIsBlackListedItem()
        {
            var gateProvider = new GateProvider(ConstructGate());

            var recordManager = new RecordManager(gateProvider, ConstructMockDataAccessLayer());
            
            Assert.IsTrue(recordManager.IsBlackListedNumber("TN 00 0000"));
            Assert.IsFalse(recordManager.IsBlackListedNumber("TN 00 9999"));
        }

        [Test]
        public void TestStartRecordingInfoTrue()
        {
            var gate = ConstructGate();

            var byteAry = new byte[] { 1, 2, 3 };
            var cameraProviders = gate.Cameras.Select(x => new MockCameraProvider(x, byteAry));

            var gateProvider = new GateProvider(gate)
            {
                CameraProviders = cameraProviders,
                ComPortProvider = new MockComProvider() { MockReadString = "S"}
            };

            VehicleBasicInfoModel vehicleBasicInfoModel;
            VehicleImagesModel vehicleImagesModel;

            // Action
            var recordManager = new RecordManager(gateProvider, ConstructMockDataAccessLayer());

            // Assert
            Assert.IsTrue(recordManager.StartRecordingInfo(out vehicleBasicInfoModel, out vehicleImagesModel));
            Assert.AreEqual(((MockComProvider)gateProvider.ComPortProvider).MockWriteString, "E");
            Assert.NotNull(vehicleBasicInfoModel.DateTime);
            Assert.AreEqual(vehicleBasicInfoModel.UniqueEntryId, vehicleImagesModel.ForeignKeyId);
        }

        [Test]
        public void TestStartRecordingInfoFalse()
        {
            var gate = ConstructGate();

            var byteAry = new byte[] { 1, 2, 3 };
            var cameraProviders = gate.Cameras.Select(x => new MockCameraProvider(x, byteAry));

            var gateProvider = new GateProvider(gate)
            {
                CameraProviders = cameraProviders,
                ComPortProvider = new MockComProvider()
            };

            VehicleBasicInfoModel vehicleBasicInfoModel;
            VehicleImagesModel vehicleImagesModel;

            // Action
            var recordManager = new RecordManager(gateProvider, ConstructMockDataAccessLayer());

            // Assert
            Assert.IsFalse(recordManager.StartRecordingInfo(out vehicleBasicInfoModel, out vehicleImagesModel));
            Assert.AreEqual(((MockComProvider)gateProvider.ComPortProvider).MockWriteString, string.Empty);
        }
    }
}
