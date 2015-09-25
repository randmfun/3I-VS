using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace SCD_UVSS.Model
{
    public class VehicleBasicInfoModel
    {
        public VehicleBasicInfoModel()
        {
            this.IsBlackListed = false;
        }

        public string UniqueEntryId { get; set; }

        public string Number { get; set; } 

        public DateTime DateTime { get; set; }

        public bool IsBlackListed { get; set; }
    }

    public class VehicleImagesModel
    {
        public VehicleImagesModel(string uniqueEntryId)
        {
            this.ForeignKeyId = uniqueEntryId;
        }

        public string ForeignKeyId
        {
            get; private set;
        }

        public byte[] DriverImage { get; set; }

        public byte[] VehicleOverallImage { get; set; }

        public byte[] NumberPlateImage { get; set; }

        public byte[] ChaisisImage { get; set; }
    }


    public enum ImageType
    {
        DriverImage,
        VehicleOverallImage,
        NumberPlateImage,
        ChaisisImage
    }
}
