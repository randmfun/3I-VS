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
        
        public bool UseCsvFile { get; set; }

        public LicenceNumberProvider(string saveLocation, bool useCSV=true)
        {
            this.SaveLocation = saveLocation;
            this.UseCsvFile = useCSV;
        }

        public string Read()
        {
            if (this.UseCsvFile)
                return this.ReadFromCsvFile();

            return this.ReadFromTextFile();
        }

        public string ReadFromCsvFile()
        {
            string retVal = "No Found";

            if (Directory.Exists(this.SaveLocation))
            {
                var directoryInfo = new DirectoryInfo(this.SaveLocation);
                var file = directoryInfo.GetFiles("*.csv").First();

                var allLines = File.ReadAllLines(file.FullName);

                var currLicencePlateModel = new LicencePlateNumberModel(allLines.Last());
                
                retVal = currLicencePlateModel.Number;
            }

            return retVal;

        }

        public string ReadFromTextFile()
        {
            string retVal = "No Found";

            if (Directory.Exists(this.SaveLocation))
            {
                var directoryInfo = new DirectoryInfo(this.SaveLocation);
                var file = directoryInfo.GetFiles("*.txt").First();

                var allLines = File.ReadAllLines(file.FullName);

                retVal = allLines.Last().Trim();
            }

            return retVal;
        }
    }

    public class LicencePlateNumberModel
    {
        private readonly string _inputLine = null;

        private readonly List<string> _inputLineList  = new List<string>();

        private string _number = null;

        public LicencePlateNumberModel(string inputLine)
        {
            this._inputLine = inputLine;
            this._inputLineList = this._inputLine.Split(',').ToList();
        }

        public string Number
        {
            get
            {
                if (this._number == null)
                {
                    this._number = this._inputLineList[2];
                    this._number = this._number.Trim('"');
                }
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        public string Time { get; set; }

        public string RecordingType { get; set; }

        public string VehicleType { get; set; }

        public string FileLocation { get; set; }
        
    }
}
