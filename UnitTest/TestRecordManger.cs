using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Controller;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;
using SCD_UVSS.SystemInput.Camera;

namespace UnitTest
{
    public class TestRecordManger
    {
        private Gate ConstructGate()
        {
            var gate = new Gate("Main Entry Gate");

            gate.Cameras.Add(new CameraModel() { Name = "Chasis One", ImageType = ImageType.ChaisisImage });
            gate.Cameras.Add(new CameraModel() { Name = "Chasis Two", ImageType = ImageType.ChaisisImage });
            gate.Cameras.Add(new CameraModel() { Name = "Top View", ImageType = ImageType.VehicleOverallImage });
            gate.Cameras.Add(new CameraModel() { Name = "Driver Image", ImageType = ImageType.DriverImage });
            gate.Cameras.Add(new CameraModel() { Name = "Licence Plate", ImageType = ImageType.NumberPlateImage });
            gate.ComPortName = "COM2";

            return gate;
        }

        [Test]
        public void TestRecordManagerConstruct()
        {
            var gateProvider = new GateProvider(this.ConstructGate());

            var recordManager = new RecordManager(gateProvider);

            Assert.AreEqual(recordManager.ContinueRecording, false);
        }

        [Test]
        public void TestRecordCurrentSnapshots()
        {
            var gate = this.ConstructGate();

            var byteAry = new byte[] {1, 2, 3};
            var cameraProviders = gate.Cameras.Select(x => new MockCameraProvider(x, byteAry));

            var gateProvider = new GateProvider(gate) {CameraProviders = cameraProviders};

            var recordManager = new RecordManager(gateProvider);

            VehicleBasicInfoModel vehicleBasicInfoModel;
            VehicleImagesModel vehicleImagesModel;

            recordManager.RecordCurrentSnapshots(out vehicleBasicInfoModel, out vehicleImagesModel);
            
            Assert.NotNull(vehicleBasicInfoModel.DateTime);
            Assert.NotNull(vehicleBasicInfoModel.UniqueEntryId);
            Assert.AreEqual(vehicleBasicInfoModel.UniqueEntryId, vehicleImagesModel.ForeignKeyId);
        }
    }
}
