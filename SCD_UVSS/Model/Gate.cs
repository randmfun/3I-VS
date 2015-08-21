using System.Collections.Generic;

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

        public string ComPort
        {
            get; set;
        }

        public List<CameraModel> Cameras
        {
            get; set;
        }
    }
}
