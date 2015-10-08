using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SCD_UVSS.SystemInput.LicenceNo
{
    public class LicenceNumberProvider
    {
        public string SaveLocation { get; set; }

        public LicenceNumberProvider(string saveLocation)
        {
            this.SaveLocation = saveLocation;
        }

        public string Read()
        {
            string retVal = "No Found";

            if (Directory.Exists(this.SaveLocation))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(this.SaveLocation);
                var file = directoryInfo.GetFiles("*.txt").First();

                var allLines = File.ReadAllLines(file.FullName);

                retVal = allLines.Last().Trim();
            }

            return retVal;
        }
    }
}
