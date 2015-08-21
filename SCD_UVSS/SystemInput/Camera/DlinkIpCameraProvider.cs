using System;
using System.Net;
using SCD_UVSS.Model;

namespace SCD_UVSS.SystemInput.Camera
{
    public class DlinkIpCameraProvider: ICameraProvider
    {
        public CameraModel CameraModel { get; set; }

        public DlinkIpCameraProvider(CameraModel cameraModel)
        {
            this.CameraModel = cameraModel;
        }

        public byte[] Read()
        {
            try
            {
                //var source = "http://localhost:5000/api/v1/boards/image";

                var source = this.CameraModel.IpAddress;
                var req = (HttpWebRequest) WebRequest.Create(source);

                using (var resp = req.GetResponse())
                {
                    using (var stream = resp.GetResponseStream())
                    {
                        byte[] bytes = stream.ToByteArray();

                        return bytes;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new EntryPointNotFoundException(exception.Message);
            }
        }
    }
}
