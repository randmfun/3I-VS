using SCD_UVSS.Model;

namespace SCD_UVSS.SystemInput.Camera
{
    public interface ICameraProvider
    {
        CameraModel CameraModel { get; set; }

        byte[] Read();
    }
}
