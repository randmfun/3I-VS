using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SCD_UVSS.Dal;
using SCD_UVSS.Model;

namespace SCD_UVSS.ViewModel
{
    public class GateSetupViewModel
    {
        public ObservableCollection<GateSetupItem> GateSetupItems { get; set; }

        public ICommand SetButtonClickCmdCommand
        {
            get; set;
        }

        private readonly DataAccessLayer _dataAccessLayer;

        public GateSetupViewModel(DataAccessLayer dataAccessLayer)
        {
            this._dataAccessLayer = dataAccessLayer;

            this.SetButtonClickCmdCommand = new RelayCommand(this.SetButtonClicked);
            
            this.GateSetupItems = new ObservableCollection<GateSetupItem>
            {
                new GateCameraSetupItem(new CameraModel(){Name = "Chasis One"}),
                new GateCameraSetupItem(new CameraModel(){Name = "Chasis Two"}),
                new GateCameraSetupItem(new CameraModel(){Name = "Top View"}),
                new GateCameraSetupItem(new CameraModel(){Name = "Driver Image"}),
                new GateCameraSetupItem(new CameraModel(){Name = "Licence Plate"}),
                new GateComPortSetupItem("COM Port")
            };
        }

        public void SetButtonClicked(object arg)
        {
            Gate gate = new Gate("Default_Gate");

            foreach (var gateSetupItem in GateSetupItems)
            {
                if (gateSetupItem is GateCameraSetupItem)
                {
                    gate.Cameras.Add(((GateCameraSetupItem)gateSetupItem).CameraModel);
                }
                else
                {
                    
                }
            }

            this._dataAccessLayer.AddGateInfo(gate);
        }
    }

    public class GateSetupItem
    {
        public string Label { get; set; }

        public string Address { get; set; }

        public GateSetupItem(string label)
        {
            this.Label = label;
        }
    }

    public class GateCameraSetupItem : GateSetupItem
    {
        public CameraModel CameraModel { get; set; }

        public GateCameraSetupItem(CameraModel cameraModel) : base(cameraModel.Name)
        {
            this.Address = "192.25.67.108";
        }
    }

    public class GateComPortSetupItem : GateSetupItem
    {
        public GateComPortSetupItem(string label) : base(label)
        {
            this.Address = "COM3";
        }
    }
}
