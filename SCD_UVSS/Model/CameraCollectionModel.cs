using System.Collections;

namespace SCD_UVSS.Model
{
    public class CameraCollectionModel: CollectionBase
    {
		// Get came at the specified index
		public Camera this[int index]
		{
			get
			{
				return ((Camera) InnerList[index]);
			}
		}

		// Add new CameraModel to the collection
		public void Add(Camera CameraModel)
		{
			InnerList.Add(CameraModel);
		}

		// Remove CameraModel from the collection
		public void Remove(Camera CameraModel)
		{
			InnerList.Remove(CameraModel);
		}

        // Get camera with specified name
        public Camera GetCamera(string name, Gate parent)
        {
            // find the camera
            foreach (Camera camera in InnerList)
            {
                if ((camera.Name == name) )
                    return camera;
            }
            return null;
        }

		// Get CameraModel with specified ID
		public Camera GetCameraModel(int CameraModelID)
		{
			// find the CameraModel
			foreach (Camera CameraModel in InnerList)
			{
				if (CameraModel.ID == CameraModelID)
					return CameraModel;
			}
			return null;
		}
    }
}
