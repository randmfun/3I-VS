using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;
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
            var gateModel = new Gate("one") { ComPortName = expecetedCom };
            gateModel.Cameras.Add(new CameraModel(){ID = expectedCamId});

            UserManager.Instance.SetLoggedInUser("super");

            var gateProvider = new GateProvider(gateModel);

            Assert.IsTrue(gateProvider.ComPortProvider != null);
            
            // TODO : Mock COM Provider
            //Assert.AreEqual(gateProvider.ComPortProvider.SerialPort.PortName, expecetedCom);
            
            Assert.IsTrue(gateProvider.CameraProviders.Count() == 1);
            Assert.AreEqual(gateProvider.CameraProviders.First().CameraModel.ID, expectedCamId);
        }
    }
}
