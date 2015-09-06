using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
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

        private Gate ConstructGate()
        {
            const int expectedCamId = 1;

            var gateModel = new Gate(GateId) { ComPortName = ExpectedCom, Name = ExpectedGateName };
            gateModel.Cameras.Add(new CameraModel() { ID = expectedCamId, IpAddress = ExpectedIpAddress});

            return gateModel;
        }

        [Test]
        public void TestSaveGateInfo()
        {
            var gate = this.ConstructGate();

            var gateSetupViewModel = new GateSetupViewModel(null);
            var status = gateSetupViewModel.SaveGateInfo(gate);

            Assert.IsTrue(status);
        }

        [Test]
        public void TestReaGateInfo()
        {
            var gate = this.ConstructGate();
            var gateSetupViewModel = new GateSetupViewModel(null);
            var status = gateSetupViewModel.SaveGateInfo(gate);

            var readGate = gateSetupViewModel.ReaSavedGateInfo();

            Assert.AreEqual(readGate.Name, ExpectedGateName);
            Assert.IsInstanceOf(typeof(CameraModel), readGate.Cameras[0]);
            Assert.AreEqual(readGate.Cameras[0].IpAddress, ExpectedIpAddress);
            Assert.AreEqual(readGate.ComPortName, ExpectedCom);
        }

        [Test]
        public void TestConstructSavedGateSetupItems()
        {
            var gate = this.ConstructGate();
            var gateSetupViewModel = new GateSetupViewModel(null);
            var gateSetupUpItems = gateSetupViewModel.ConstructSavedGateSetupItems(gate);
            Assert.AreEqual(gateSetupUpItems.Count, 3);

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
            }
        }

        [Test]
        public void TestConstructGateModelFromGateSetupItems()
        {
            var gate = this.ConstructGate();
            var gateSetupViewModel = new GateSetupViewModel(null);
            var gateSetupUpItems = gateSetupViewModel.ConstructSavedGateSetupItems(gate);

            var gateNew = gateSetupViewModel.ConstructGateModelFromGateSetupItems(gateSetupUpItems);

            Assert.AreEqual(gateNew.ComPortName, ExpectedCom);
            Assert.AreEqual(gateNew.Name, ExpectedGateName);
            Assert.AreEqual(gateNew.Cameras.Count, 1);
        }
    }
}
