using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.SystemInput.LicenceNo;

namespace UnitTest
{
    public class TestLicenceNumberProvider
    {
        [Test]
        public void TestRead()
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var licSaveLoc = Path.Combine(executiongDir, "Resources", "LicenceNoLoc");
            var licenceNumberProvider = new LicenceNumberProvider(licSaveLoc);
            var licenceNumber = licenceNumberProvider.Read();

            Assert.AreEqual(licenceNumber, "TN 08 5678");
        }
    }
}
