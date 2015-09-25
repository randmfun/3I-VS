using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.SystemInput.Camera
{
    public class MockCameraProvider : ICameraProvider
    {
        public MockCameraProvider(CameraModel cameraModel, byte[] readByteAry)
        {
            this.CameraModel = cameraModel;
            this._readArrayContent = readByteAry;
        }

        public CameraModel CameraModel { get; set; }

        private readonly byte[] _readArrayContent;

        public byte[] Read()
        {
            return this._readArrayContent;
        }
    }
}
