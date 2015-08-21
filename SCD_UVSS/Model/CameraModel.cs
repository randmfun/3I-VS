namespace SCD_UVSS.Model
{
    public class CameraModel
    {
        private string description = "";

        public CameraModel()
        {
            ID = 0;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string IsDualCam { get; set; }

        public string IpAddress
        {
            get; set;
        }

        public string SecondIpAddress
        {
            get; set;
        }
    }
}
