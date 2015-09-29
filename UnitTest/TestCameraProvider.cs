using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput.Camera;

namespace UnitTest
{
    public class TestCameraProvider
    {
        private string source = "http://www.atmel.com/Images/SAM_D21_Xplained_Pro_angle.png";
        
        //[Test]
        public void TestRead()
        {
            var camModel = new CameraModel() {IpAddress = source};
            var camProvider = new DlinkIpCameraProvider(camModel);

            var bytearry = camProvider.Read();
            
        }
    }
}
