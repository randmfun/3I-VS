namespace SCD_UVSS.Model
{
    public class CameraModel
    {
        private string name;
        private string description = "";
        private CameraGroupModel _groupModel;

        public CameraModel()
        {
            ID = 0;
        }

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
        public CameraGroupModel Group
        {
            get { return _groupModel; }
            set { _groupModel = value; }
        }

        // FullName property
        public string FullName
        {
            get
            {
                return (this._groupModel == null) ? name : (this._groupModel.FullName + '\\' + name);
            }
        }

        public object Configuration { get; set; }
    }
}
