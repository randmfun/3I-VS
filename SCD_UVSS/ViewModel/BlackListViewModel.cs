using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using log4net;
using SCD_UVSS.Dal;
using SCD_UVSS.Model;

namespace SCD_UVSS.ViewModel
{
    public class BlackListViewModel
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GateSetupViewModel));

        private readonly DataAccessLayer dataAccessLayer;

        public string SearchVehicleNumber { get; set; }

        public string AddVehicleNumber { get; set; }

        public ICommand CmdAddBlackListedItem { get; set; }

        public ICommand CmdSearchVehicleNumber { get; set; }
        
        public ObservableCollection<BlackListItem> SearchResults { get; set;}
        

        public BlackListViewModel(DataAccessLayer dataAccessLayer)
        {
            this.dataAccessLayer = dataAccessLayer;
            
            this.SearchVehicleNumber = string.Empty;
            this.AddVehicleNumber = string.Empty;

            this.SearchResults = new ObservableCollection<BlackListItem>();
        
            this.CmdAddBlackListedItem = new RelayCommand(AddBlackListItemHandler);
            this.CmdSearchVehicleNumber = new RelayCommand(SearchBlackListItemsHandler);
        }

        public void AddBlackListItemHandler(object dummy)
        {
            logger.Info(string.Format("Adding vehichle to Blacklist : {0}", this.AddVehicleNumber));

            if (string.IsNullOrEmpty(this.AddVehicleNumber)) return;

            this.dataAccessLayer.AddBlackListItem(new BlackListItem() {VehicleNumber = this.AddVehicleNumber});
            logger.Info(string.Format("Successfully Added vehichle to Blacklist : {0}", this.AddVehicleNumber));
        }

        public void SearchBlackListItemsHandler(object dummy)
        {
            var results = this.dataAccessLayer.GetAllBlackListItem();
            var filteredResults = this.GetFilteredList(results, this.SearchVehicleNumber);

            this.SearchResults.Clear();
            filteredResults.ToList().ForEach(item => this.SearchResults.Add(item));
        }

        private IEnumerable<BlackListItem> GetFilteredList(IEnumerable<BlackListItem> searchResults, string searchText)
        {
            return from blackListItem in searchResults
                   where blackListItem.VehicleNumber.ToLower().Contains(searchText.ToLower())
                   select blackListItem;
        }
    }
}
