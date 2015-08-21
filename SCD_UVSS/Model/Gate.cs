using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using SCD_UVSS.SystemInput.Camera;
using SCD_UVSS.SystemInput.COM;

namespace SCD_UVSS.Model
{
    /// <summary>
    /// Gate has a collection of Cameras
    /// </summary>
    public class Gate
    {
        private string	name;
		private string	description = "";

        public Gate(string name)
        {
            ID = 0;
            this.name = name;
            this.Cameras = new List<CameraModel>();
        }

        public int ID { get; set; }

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

        public SerialPort ComPort
        {
            get; set;
        }

        public List<CameraModel> Cameras
        {
            get; set;
        }
    }

    public class GateProvider
    {
        public readonly Gate Gate;

        private readonly ComPortProvider _comPortProvider;

        private readonly IEnumerable<ICameraProvider> _cameraProviders;
 
        public GateProvider(Gate gate)
        {
            this.Gate = gate;

            this._comPortProvider = new ComPortProvider(this.Gate.ComPort);
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
