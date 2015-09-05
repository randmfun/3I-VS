using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Input;
using log4net;
using SCD_UVSS.Dal;
using SCD_UVSS.helpers;
using SCD_UVSS.Model;
using SCD_UVSS.SystemInput.COM;

namespace SCD_UVSS.ViewModel
{
    public class GateSetupViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GateSetupViewModel));

        public const string GateInfoFileName = "gateinfo.bin";

        private ObservableCollection<GateSetupItem> _gateSetupItems;

        public ObservableCollection<GateSetupItem> GateSetupItems
        {
            get
            {
                if (this._gateSetupItems == null)
                    this._gateSetupItems = File.Exists(GateInfoFileName) ? 
                        this.ConstructSavedGateSetupItems(this.ReaSavedGateInfo()) : this.ConstructDummyGateSetupItems();
                return this._gateSetupItems;
            } 
            set { this._gateSetupItems = value; }
        }

        public ICommand SaveButtonClickCmdCommand
        {
            get; set;
        }

        private readonly DataAccessLayer _dataAccessLayer;

        public GateSetupViewModel(DataAccessLayer dataAccessLayer)
        {
            this._dataAccessLayer = dataAccessLayer;
            this.SaveButtonClickCmdCommand = new RelayCommand(this.SaveButtonClickHandler);
        }
        
        public void SaveButtonClickHandler(object arg)
        {
            var gate = this.ConstructGateModelFromGateSetupItems(this.GateSetupItems);

            //this._dataAccessLayer.AddGateInfo(gate);
            SaveGateInfo(gate);
        }

        public bool SaveGateInfo(Gate gate)
        {
            return SerializeUtility.Save(gate, GateInfoFileName);
        }

        public Gate ReaSavedGateInfo()
        {
            return SerializeUtility.Read<Gate>(GateInfoFileName);
        }

        public Gate ConstructGateModelFromGateSetupItems(ObservableCollection<GateSetupItem> gateSetupItems)
        {
            var gate = new Gate("Default_Gate");

            foreach (var gateSetupItem in gateSetupItems)
            {
                if (gateSetupItem is GateCameraSetupItem)
                {
                    gate.Cameras.Add(((GateCameraSetupItem)gateSetupItem).CameraModel);
                }
                else if (gateSetupItem is GateComPortSetupItem)
                {
                    gate.ComPortName = gateSetupItem.Address;
                }
                else if (gateSetupItem is GateNameSetupItem)
                {
                    gate.Name = gateSetupItem.Address;
                }
            }

            return gate;
        }

        public ObservableCollection<GateSetupItem> ConstructSavedGateSetupItems(Gate gateInfo)
        {
            var gateSetupItems = new ObservableCollection<GateSetupItem>
            {
                new GateNameSetupItem(gateInfo.Name),
                new GateComPortSetupItem(gateInfo.ComPortName)
            };

            foreach (var camera in gateInfo.Cameras)
            {
                gateSetupItems.Add(new GateCameraSetupItem(camera));
            }

            return gateSetupItems;
        }
        
        private ObservableCollection<GateSetupItem> ConstructDummyGateSetupItems()
        {
            var gate = new Gate("Default_Gate");
            gate.Cameras.Add(new CameraModel() { Name = "Chasis One" });
            gate.Cameras.Add(new CameraModel() { Name = "Chasis Two" });
            gate.Cameras.Add(new CameraModel() { Name = "Top View" });
            gate.Cameras.Add(new CameraModel() { Name = "Driver Image" });
            gate.Cameras.Add(new CameraModel() { Name = "Licence Plate" });
            gate.ComPortName = "COM Port Name";

            return ConstructSavedGateSetupItems(gate);
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

        public new string Address 
        {
            get { return this.CameraModel.IpAddress; } 
            set { this.CameraModel.IpAddress = value; }
        }

        public GateCameraSetupItem(CameraModel cameraModel) : base(cameraModel.Name)
        {
            this.CameraModel = cameraModel;
        }
    }

    public class GateComPortSetupItem : GateSetupItem
    {
        public GateComPortSetupItem(string address) : base("COM Port")
        {
            this.Address = address;
        }
    }

    public class GateNameSetupItem : GateSetupItem
    {
        public GateNameSetupItem(string address)
            : base("Gate Name")
        {
            this.Address = address;
        }
    }
}
