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

        private readonly ComPortProvider _comPortProvider;

        private readonly IEnumerable<ICameraProvider> _cameraProviders;

        public GateProvider(Gate gate)
        {
            this.Gate = gate;

            this._comPortProvider = new ComPortProvider(new SerialPort(this.Gate.ComPortName));
            this._cameraProviders = this.Gate.Cameras.Select(x => new DlinkIpCameraProvider(x));
        }

        public ComPortProvider ComPortProvider
        {
            get { return this._comPortProvider; }
        }

        public IEnumerable<ICameraProvider> CameraProviders
        {
            get
            {
                return this._cameraProviders;
            }
        }
    }
}
