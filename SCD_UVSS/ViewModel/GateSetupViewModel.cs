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
                new GateCameraSetupItem(new Camera()),
                new GateCameraSetupItem(new Camera()),
                new GateComPortSetupItem("Com Port")
            };
        }

        public void SetButtonClicked(object arg)
        {
            Gate gate = new Gate("Default_Gate");

            foreach (var gateSetupItem in GateSetupItems)
            {
                if (gateSetupItem is GateCameraSetupItem)
                {
                    gate.Cameras.Add(((GateCameraSetupItem)gateSetupItem).Camera);
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

        public GateSetupItem(string label)
        {
            this.Label = label;
        }
    }

    public class GateCameraSetupItem : GateSetupItem
    {
        public Camera Camera { get; set; }

        public GateCameraSetupItem(Camera camera) : base(camera.Name)
        {
            
        }
    }

    public class GateComPortSetupItem : GateSetupItem
    {
        public GateComPortSetupItem(string label) : base(label)
        {
            
        }
    }
}
