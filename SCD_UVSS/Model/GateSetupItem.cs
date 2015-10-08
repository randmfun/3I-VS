using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCD_UVSS.Model
{
    public class GateSetupItem
    {
        public string Label { get; set; }

        public string Address { get; set; }

        public string SavePath { get; set; }

        public bool IsPathVisible { get; private set; }

        public GateSetupItem(string label)
        {
            this.Label = label;
        }
    }

    public class GateCameraSetupItem : GateSetupItem
    {
        public CameraModel CameraModel { get; set; }

        public new string Address
        {
            get { return this.CameraModel.IpAddress; }
            set { this.CameraModel.IpAddress = value; }
        }

        public new string SavePath
        {
            get { return this.CameraModel.SavePath; }
            set { this.CameraModel.SavePath = value; }
        }

        public new bool IsPathVisible
        {
            get { return true; }
        }

        public GateCameraSetupItem(CameraModel cameraModel)
            : base(cameraModel.Name)
        {
            this.CameraModel = cameraModel;
        }
    }

    public class GateComPortSetupItem : GateSetupItem
    {
        public GateComPortSetupItem(string address)
            : base("COM Port :")
        {
            this.Address = address;
        }
    }

    public class GateNameSetupItem : GateSetupItem
    {
        public GateNameSetupItem(string address)
            : base("Gate Name :")
        {
            this.Address = address;
        }
    }

    public class GateVehicleNumberSaveLoc : GateSetupItem
    {
        public GateVehicleNumberSaveLoc(string address)
            : base("Vehicle Number Save Location : ")
        {
            this.Address = address;
        }
    }

}
