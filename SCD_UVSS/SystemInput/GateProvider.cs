using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace SCD_UVSS.SystemInput
{
    public class GateProvider
    {
        public readonly Gate Gate;

        private IComPortProvider _comPortProvider;

        private IEnumerable<ICameraProvider> _cameraProviders;

        public GateProvider(Gate gate)
        {
            this.Gate = gate;

            this._comPortProvider = new ComPortProvider(new SerialPort(this.Gate.ComPortName));
            this._cameraProviders = this.Gate.Cameras.Select(x => new DlinkIpCameraProvider(x));
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
