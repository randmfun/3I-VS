using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.SystemInput
{
    public class DlinkIpCameraProvider
    {
        public readonly CameraModel _cameraModel;

        public DlinkIpCameraProvider(CameraModel cameraModel)
        {
            this._cameraModel = cameraModel;
        }

        public byte[] Read()
        {
            try
            {
                //var source = "http://localhost:5000/api/v1/boards/image";

                var source = this._cameraModel.IpAddress;
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
