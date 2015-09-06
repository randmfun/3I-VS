using System;
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
    [Serializable]
    public class Gate
    {
        private string	name;
		private string	description = "";

        public Gate(string name, string id = null)
        {
            ID = id ?? Guid.NewGuid().ToString();
            this.name = name;
            this.Cameras = new List<CameraModel>();
        }

        public string ID { get; set; }

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

        public string ComPortName
        {
            get; set;
        }

        public List<CameraModel> Cameras
        {
            get; set;
        }
    }

}
