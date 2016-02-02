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
        public void TestReadFromTxtFile()
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var licSaveLoc = Path.Combine(executiongDir, "Resources", "LicenceNoLoc");
            var licenceNumberProvider = new LicenceNumberProvider(licSaveLoc, false);
            var licenceNumber = licenceNumberProvider.Read();
            
            Assert.AreEqual(licenceNumber, "TN 08 5678");
        }

        [Test]
        public void TestReadFromCsvFile()
        {
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var licSaveLoc = Path.Combine(executiongDir, "Resources", "LicenceNoLoc");
            var licenceNumberProvider = new LicenceNumberProvider(licSaveLoc);
            var licenceNumber = licenceNumberProvider.Read();

            Assert.AreEqual(licenceNumber, "EM458$");
        }

        [Test]
        public void TestLicencePlateNumberModelShortString()
        {
            const string shortString = "\"11/13/2015 10:45:32 AM\",\"Video File 1\",\"EM458$\",\"LV\",\"\"";
            var currLicencePlateModel = new LicencePlateNumberModel(shortString);

            Assert.AreEqual(currLicencePlateModel.Number, "EM458$");
        }

        [Test]
        public void TestLicencePlateNumberModelLongString()
        {
            const string shortString = "\"11/13/2015 10:45:32 AM\",\"Video File 1\",\"EM458$\",\"LV\",\"C:\\Users\\Srini\\Desktop\\Vehicle NUmber Plate Images\\2015-11-13\\2015-11-13-10-45-32-059_EM458$.jpg\"";
            var currLicencePlateModel = new LicencePlateNumberModel(shortString);

            Assert.AreEqual(currLicencePlateModel.Number, "EM458$");
        }

    }
}
