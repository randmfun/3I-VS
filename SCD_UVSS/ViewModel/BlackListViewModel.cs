using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using log4net;
using SCD_UVSS.Dal;
using SCD_UVSS.Model;

namespace SCD_UVSS.ViewModel
{
    public class BlackListViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GateSetupViewModel));

        private readonly DataAccessLayer _dataAccessLayer;

        public string SearchVehicleNumber { get; set; }

        public string AddVehicleNumber { get; set; }

        public ICommand CmdAddBlackListedItem { get; set; }

        public ICommand CmdSearchVehicleNumber { get; set; }
        
        public ObservableCollection<BlackListItem> SearchResults { get; set;}

        public BlackListViewModel(DataAccessLayer dataAccessLayer)
        {
            this._dataAccessLayer = dataAccessLayer;
            
            this.SearchVehicleNumber = string.Empty;
            this.AddVehicleNumber = string.Empty;

            this.SearchResults = new ObservableCollection<BlackListItem>();
        
            this.CmdAddBlackListedItem = new RelayCommand(AddBlackListItemHandler);
            this.CmdSearchVehicleNumber = new RelayCommand(SearchBlackListItemsHandler);
        }

        public void AddBlackListItemHandler(object dummy)
        {
            Logger.Info(string.Format("Adding vehichle to Blacklist : {0}", this.AddVehicleNumber));
            
            if (string.IsNullOrEmpty(this.AddVehicleNumber)) return;
            try
            {
                this._dataAccessLayer.AddBlackListItem(new BlackListItem() {VehicleNumber = this.AddVehicleNumber});
            }
            catch (Exception exception)
            {
                Logger.Error("AddBlackListItemHandler Failed", exception);
                Logger.Error(exception.Message, exception.InnerException);
                throw;
            }
            Logger.Info(string.Format("Successfully Added vehichle to Blacklist : {0}", this.AddVehicleNumber));
            this.AddVehicleNumber = string.Empty;
        }

        public void SearchBlackListItemsHandler(object dummy)
        {
            Logger.Info("Search black list item");

            try
            {
                var results = this._dataAccessLayer.GetAllBlackListItem();
                var filteredResults = this.GetFilteredList(results, this.SearchVehicleNumber);

                this.SearchResults.Clear();
                filteredResults.ToList().ForEach(item => this.SearchResults.Add(item));
            }
            catch (Exception exception)
            {
                Logger.Error("SearchBlackListItemsHandler Failed", exception);
                Logger.Error(exception.Message, exception.InnerException);
                throw;
            }
        }

        private IEnumerable<BlackListItem> GetFilteredList(IEnumerable<BlackListItem> searchResults, string searchText)
        {
            return from blackListItem in searchResults
                   where blackListItem.VehicleNumber.ToLower().Contains(searchText.ToLower())
                   select blackListItem;
        }
    }
}
