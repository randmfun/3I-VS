using System;

namespace SCD_UVSS.Model
{
    [Serializable]
    public class CameraModel
    {
        public CameraModel()
        {
            this.ID = 0;
            this.Description = string.Empty;
            this.IpAddress = "255.255.255.255";
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IsDualCam { get; set; }

        public string SavePath { get; set; }

        public string IpAddress
        {
            get; set;
        }

        public ImageType ImageType { get; set; }
    }
}
