﻿using System;
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

        public Guid UniqueId { get; set; }

        public string Number { get; set; } 

        public DateTime DateTime { get; set; }

        public bool IsBlackListed { get; set; }
    }

    public class VehicleImagesModel
    {
        private readonly VehicleBasicInfoModel _vehicleBasicInfo;

        public VehicleImagesModel(VehicleBasicInfoModel vehicleBasicInfo)
        {
            this._vehicleBasicInfo = vehicleBasicInfo;
        }

        public Guid ForeignKeyId
        {
            get { return this._vehicleBasicInfo.UniqueId; }
        }

        public BitmapImage DriverImage { get; set; }

        public BitmapImage VehicleOverallImage { get; set; }

        public BitmapImage NumberPlateImage { get; set; }

        public BitmapImage ChaisisImage { get; set; }
    }
}
