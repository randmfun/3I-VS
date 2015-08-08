namespace SCD_UVSS.Model
{
    public class CameraGroupModel
    {
        private string	name;
		private string	description = "";
		private CameraGroupModel	parent = null;

		// ID property
        public int ID { get; set; }

        // Name property
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		// Description property
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		// Parent property
		public CameraGroupModel Parent
		{
			get { return parent; }
			set { parent = value; }
		}
		// FullName property
		public string FullName
		{
			get
			{
				return (parent == null) ? name : (parent.FullName + '\\' + name);
			}
		}

		// Constructor
        public CameraGroupModel(string name)
        {
            ID = 0;
            this.name = name;
        }
    }
}
