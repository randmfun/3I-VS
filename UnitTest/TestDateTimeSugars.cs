using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.helpers;

namespace UnitTest
{
    public class TestDateTimeSugars
    {
        [Test]
        public void TestConvertToMySqlFormat()
        {
            var dateTime = new DateTime(2015, 10, 3, 23, 59, 59, 59);

            var mySqlDateTimeFormat = dateTime.ConvertToMySqlFormat();

            Assert.AreEqual(mySqlDateTimeFormat, "2015-10-03 23:59:59");
        }

        [Test]
        public void TestGetDatePortion()
        {
            var dateTime = new DateTime(2015, 10, 3, 23, 59, 59, 59);

            var datePortion = dateTime.GetDatePortion();

            Assert.AreEqual(datePortion, "2015-10-03");
        }

        [Test]
        public void TestGetTimePortion()
        {
            var dateTime = new DateTime(2015, 10, 3, 23, 59, 59, 59);

            var datePortion = dateTime.GetTimePortion();

            Assert.AreEqual(datePortion, "23:59:59");
        }
    }
}
