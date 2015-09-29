using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace SCD_UVSS.SystemInput.COM
{
    public class MockComProvider : IComPortProvider
    {
        public string MockReadString = string.Empty;

        public string MockWriteString = string.Empty;

        public SerialPort SerialPort { get; set; }


        public string Read()
        {
            return MockReadString;
        }

        public void Write(string message)
        {
            this.MockWriteString = message;
        }
    }
}
