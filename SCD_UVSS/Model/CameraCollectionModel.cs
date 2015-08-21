using System.Collections;

namespace SCD_UVSS.Model
{
    public class CameraCollectionModel: CollectionBase
    {
		// Get came at the specified index
		public CameraModel this[int index]
		{
			get
			{
				return ((CameraModel) InnerList[index]);
			}
		}

		// Add new CameraModel to the collection
		public void Add(CameraModel CameraModel)
		{
			InnerList.Add(CameraModel);
		}

		// Remove CameraModel from the collection
		public void Remove(CameraModel CameraModel)
		{
			InnerList.Remove(CameraModel);
		}

        // Get CameraModel with specified name
        public CameraModel GetCamera(string name, Gate parent)
        {
            // find the CameraModel
            foreach (CameraModel camera in InnerList)
            {
                if ((camera.Name == name) )
                    return camera;
            }
            return null;
        }

		// Get CameraModel with specified ID
		public CameraModel GetCameraModel(int CameraModelID)
		{
			// find the CameraModel
			foreach (CameraModel CameraModel in InnerList)
			{
				if (CameraModel.ID == CameraModelID)
					return CameraModel;
			}
			return null;
		}
    }
}
