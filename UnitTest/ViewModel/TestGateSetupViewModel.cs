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
        private const string expected_gate_name = "Test Gate";
        private const string expectedCom = "COM1";
        private const string expectedIpAddress = "255.255.255.255";

        private Gate ConstructGate()
        {
            const int expectedCamId = 1;

            var gateModel = new Gate("one") { ComPortName = expectedCom, Name = expected_gate_name };
            gateModel.Cameras.Add(new CameraModel() { ID = expectedCamId, IpAddress = expectedIpAddress});

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

            Assert.AreEqual(readGate.Name, expected_gate_name);
            Assert.IsInstanceOf(typeof(CameraModel), readGate.Cameras[0]);
            Assert.AreEqual(readGate.Cameras[0].IpAddress, expectedIpAddress);
            Assert.AreEqual(readGate.ComPortName, expectedCom);
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
                    Assert.AreEqual(gateSetupItem.Address, expectedCom);
                }
                else if (gateSetupItem is GateNameSetupItem)
                {
                    Assert.AreEqual(gateSetupItem.Address, expected_gate_name);
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

            Assert.AreEqual(gateNew.ComPortName, expectedCom);
            Assert.AreEqual(gateNew.Name, expected_gate_name);
            Assert.AreEqual(gateNew.Cameras.Count, 1);
        }
    }
}
