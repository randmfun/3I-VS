using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;
using SCD_UVSS.ViewModel;

namespace UnitTest.ViewModel
{
    public class TestGateSetupViewModel
    {
        private const string ExpectedGateName = "Test Gate";
        private const string ExpectedCom = "COM1";
        private const string ExpectedIpAddress = "255.255.255.255";
        private const string GateId = "Gate_ID";
        private const string ExpectedSavePath = @"F:\save\camerapic\here";
        private const string ExpectedVehicleNumberSavePath = @"F:\save\vehiclenumber\here";

        private Gate ConstructGate()
        {
            const int expectedCamId = 1;

            var gateModel = new Gate(GateId) { ComPortName = ExpectedCom, Name = ExpectedGateName, VehicleNumberSaveFolder = ExpectedVehicleNumberSavePath };
            gateModel.Cameras.Add(new CameraModel() { ID = expectedCamId, IpAddress = ExpectedIpAddress, SavePath = ExpectedSavePath });

            return gateModel;
        }

        private GateSetupViewModel ConstructGateSetupViewModel()
        {
            return new GateSetupViewModel(new DataAccessLayer(new MockDatabaseProvider()));
        }

        [Test]
        public void TestSaveGateInfo()
        {
            var gate = this.ConstructGate();

            var gateSetupViewModel = this.ConstructGateSetupViewModel();
            var status = gateSetupViewModel.SaveGateInfo(gate);

            Assert.IsTrue(status);
        }

        [Test]
        public void TestReaGateInfo()
        {
            var gate = this.ConstructGate();
            var gateSetupViewModel = this.ConstructGateSetupViewModel();
            
            gateSetupViewModel.SaveGateInfo(gate);

            var readGate = gateSetupViewModel.ReadSavedGateInfo();

            Assert.AreEqual(readGate.Name, ExpectedGateName);
            Assert.IsInstanceOf(typeof(CameraModel), readGate.Cameras[0]);
            Assert.AreEqual(readGate.Cameras[0].IpAddress, ExpectedIpAddress);
            Assert.AreEqual(readGate.Cameras[0].SavePath, ExpectedSavePath);
            Assert.AreEqual(readGate.VehicleNumberSaveFolder, ExpectedVehicleNumberSavePath);
            Assert.AreEqual(readGate.ComPortName, ExpectedCom);
        }

        [Test]
        public void TestConstructSavedGateSetupItems()
        {
            var gate = this.ConstructGate();
            var gateSetupViewModel = this.ConstructGateSetupViewModel();
            var gateSetupUpItems = gateSetupViewModel.ConstructSavedGateSetupItems(gate);
            Assert.AreEqual(gateSetupUpItems.Count, 4);

            foreach (var gateSetupItem in gateSetupUpItems)
            {
                if (gateSetupItem is GateComPortSetupItem)
                {
                    Assert.AreEqual(gateSetupItem.Address, ExpectedCom);
                }
                else if (gateSetupItem is GateNameSetupItem)
                {
                    Assert.AreEqual(gateSetupItem.Address, ExpectedGateName);
                }
                else if(gateSetupItem is GateCameraSetupItem)
                {
                    Assert.AreEqual(((GateCameraSetupItem)(gateSetupItem)).SavePath, ExpectedSavePath);
                }
                else if (gateSetupItem is GateVehicleNumberSaveLoc)
                {
                    Assert.AreEqual(gateSetupItem.Address, ExpectedVehicleNumberSavePath);
                }
            }
        }

        [Test]
        public void TestConstructGateModelFromGateSetupItems()
        {
            var gate = this.ConstructGate();
            var gateSetupViewModel = this.ConstructGateSetupViewModel();
            var gateSetupUpItems = gateSetupViewModel.ConstructSavedGateSetupItems(gate);

            var gateNew = gateSetupViewModel.ConstructGateModelFromGateSetupItems(gateSetupUpItems);

            Assert.AreEqual(gateNew.ComPortName, ExpectedCom);
            Assert.AreEqual(gateNew.Name, ExpectedGateName);
            Assert.AreEqual(gateNew.VehicleNumberSaveFolder, ExpectedVehicleNumberSavePath);
            Assert.AreEqual(gateNew.Cameras.Count, 1);
        }
    }
}
