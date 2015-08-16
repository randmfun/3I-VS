namespace SCD_UVSS.Model
{
    public class Camera
    {
        private string description = "";

        public Camera()
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

        public string IpAddress
        {
            get; set;
        }

    }
}
