using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Controller;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput;

namespace UnitTest
{
    public class TestRecordManger
    {
        [Test]
        public void TestRecordManagerConstruct()
        {
            var gateProvider = new GateProvider(new Gate("one"));

            var recordManager = new RecordManager(gateProvider);

            Assert.AreEqual(recordManager.ContinueRecording, false);
        }
    }
}
