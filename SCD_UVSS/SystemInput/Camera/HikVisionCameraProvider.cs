using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.SystemInput.Camera
{
    public class HikVisionCameraProvider : ICameraProvider
    {
        public HikVisionCameraProvider(CameraModel cameraModel)
        {
            this.CameraModel = cameraModel;
        }

        public CameraModel CameraModel { get; set; }

        public byte[] Read()
        {
            var file = GetLatestFile(this.CameraModel.SavePath);
            return File.ReadAllBytes(file.FullName);
        }

        public FileInfo GetLatestFile(string directoryName)
        {
            var dirInfo = new DirectoryInfo(directoryName);

            return (from file in dirInfo.GetFiles()
                orderby file.CreationTime descending
                select file).First();
        }

    }
}
