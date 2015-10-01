using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput.Camera;

namespace UnitTest
{
    public class TestHikVisionCameraProvider
    {
        [Test]
        public void GetLatestFileInADir()
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var checkDir = Path.Combine(executiongDir, "Resources");

            var hikProvider = new HikVisionCameraProvider(null);
            var latestFile = hikProvider.GetLatestFile(checkDir);

            Assert.AreEqual(latestFile.Name, "split_2.jpg");
        }

        [Test]
        public void ReadContent()
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dir = Path.Combine(executiongDir, "Resources");

            var camModel = new CameraModel() {SavePath = dir};
            var hikProvider = new HikVisionCameraProvider(camModel);
            var bytes = hikProvider.Read();

            Assert.True(bytes.Length > 4000);
        }
    }
}
