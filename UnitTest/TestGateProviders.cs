using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace UnitTest
{
    public class TestGateProviders
    {
        [Test]
        public void TestGateProviderConstruct()
        {
            const string expecetedCom = "COM1";
            const int expectedCamId = 1;
            var gateModel = new Gate("one") { ComPort = new SerialPort(expecetedCom) };
            gateModel.Cameras.Add(new CameraModel(){ID = expectedCamId});

            var gateProvider = new GateProvider(gateModel);

            Assert.IsTrue(gateProvider.ComPortProvider != null);
            Assert.AreEqual(gateProvider.ComPortProvider.SerialPort.PortName, expecetedCom);
            Assert.IsTrue(gateProvider.CameraProviders.Count() == 1);
            Assert.AreEqual(gateProvider.CameraProviders.First().CameraModel.ID, expectedCamId);
        }
    }
}
