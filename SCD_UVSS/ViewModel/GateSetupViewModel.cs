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

        public string GateInfoFileName
        {
            get { return this._dataAccessLayer.GateInfoFileName; }
        }

        public event ErrorMessageDelegate ShowMessage;

        private ObservableCollection<GateSetupItem> _gateSetupItems;

        public ObservableCollection<GateSetupItem> GateSetupItems
        {
            get {
                return this._gateSetupItems ??
                       (this._gateSetupItems = this.ConstructSavedGateSetupItems(this.ReadSavedGateInfo()));
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

            var errors = this._dataAccessLayer.ValidateGateInformation(gate);
            if (errors.Length > 0)
            {
                if (this.ShowMessage != null)
                    this.ShowMessage(string.Format("Errors {0}", errors.ToString()));
                return;
            }

            SaveGateInfo(gate);

            if (this.ShowMessage != null)
                this.ShowMessage("Save Successful !!");
        }

        public bool SaveGateInfo(Gate gate)
        {
            return this._dataAccessLayer.SaveGateInfo(gate);
        }

        public Gate ReadSavedGateInfo()
        {
            return  this._dataAccessLayer.ReadGateInfo();
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
                else if (gateSetupItem is GateVehicleNumberSaveLoc)
                {
                    gate.VehicleNumberSaveFolder = gateSetupItem.Address;
                }
            }

            return gate;
        }

        public ObservableCollection<GateSetupItem> ConstructSavedGateSetupItems(Gate gateInfo)
        {
            Logger.Info("Found Saved Gate inforation: " + GateInfoFileName);

            try
            {
                var gateSetupItems = new ObservableCollection<GateSetupItem>
                {
                    new GateNameSetupItem(gateInfo.Name),
                    new GateComPortSetupItem(gateInfo.ComPortName),
                    new GateVehicleNumberSaveLoc(gateInfo.VehicleNumberSaveFolder)
                };

                foreach (var camera in gateInfo.Cameras)
                {
                    gateSetupItems.Add(new GateCameraSetupItem(camera));
                }

                return gateSetupItems;
            }
            catch (Exception exception)
            {
                Logger.Fatal("Reading saved gate information failed", exception);
                // Fall back
                return ConstructSavedGateSetupItems(this._dataAccessLayer.ReadGateInfo());
            }
        }
    }

}
