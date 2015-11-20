using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;
using SCD_UVSS.SystemInput.LicenceNo;

namespace SCD_UVSS.SystemInput
{
    public class GateProvider
    {
        public readonly Gate Gate;

        private IComPortProvider _comPortProvider;

        private LicenceNumberProvider _licenceNumberProvider;

        private IEnumerable<ICameraProvider> _cameraProviders;

        public GateProvider(Gate gate)
        {
            this.Gate = gate;

            //this._comPortProvider = new ComPortProvider(new SerialPort(this.Gate.ComPortName));
            this._comPortProvider = new MockComProvider(){MockReadString = "S"};
            this._cameraProviders = this.Gate.Cameras.Select(x => new HikVisionCameraProvider(x));
            this._licenceNumberProvider = new LicenceNumberProvider(this.Gate.VehicleNumberSaveFolder);
        }

        public LicenceNumberProvider LicenceNumberProvider
        {
            get { return this._licenceNumberProvider; }
            set { this._licenceNumberProvider = value; }
        }

        public IComPortProvider ComPortProvider
        {
            get { return this._comPortProvider; }
            set { this._comPortProvider = value; }
        }

        public IEnumerable<ICameraProvider> CameraProviders
        {
            get
            {
                return this._cameraProviders;
            }
            set
            {
                this._cameraProviders = value;
            }
        }
    }
}
