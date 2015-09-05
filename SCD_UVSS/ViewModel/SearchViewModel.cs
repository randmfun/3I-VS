using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;
using SCD_UVSS.View;

namespace SCD_UVSS.ViewModel
{
    public class SearchViewModel
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public ICommand SearchCommand { get; set; }

        public SearchViewModel(DataAccessLayer dataAccessLayer) : this()
        {
            this._dataAccessLayer = dataAccessLayer;
        }
        
        private SearchViewModel()
        {
            this.SearchDataList = new ObservableCollection<SearchDataViewItem>();
            this.StartTimeSpanViewModel = new TimeSpanViewModel();
            this.EndTimeSpanViewModel = new TimeSpanViewModel();

            this.SearchCommand = new RelayCommand(Search);
            this.StartDateTime = DateTime.Now;
            this.EndDateTime = DateTime.Now;

            this.VehicleNumber = "";
        }

        public TimeSpanViewModel StartTimeSpanViewModel { get; set; }
        
        public TimeSpanViewModel EndTimeSpanViewModel { get; set; }
        
        public string VehicleNumber { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set;}

        public ObservableCollection<SearchDataViewItem> SearchDataList
        {
            get; set;
        }

        public void Search(object arg)
        {
            var startDateWithTime = new DateTime(this.StartDateTime.Year, this.StartDateTime.Month, this.StartDateTime.Day, 
                int.Parse(this.StartTimeSpanViewModel.SelectedHour), int.Parse(this.StartTimeSpanViewModel.SelectedMinute), 0);

            var endDateWithTime = new DateTime(this.EndDateTime.Year, this.EndDateTime.Month, this.EndDateTime.Day,
                int.Parse(this.EndTimeSpanViewModel.SelectedHour), int.Parse(this.EndTimeSpanViewModel.SelectedMinute), 0);

            var dalSearchModel = new DbSearchRequestModel()
            {
                StaDateTime = startDateWithTime,
                EnDateTime = endDateWithTime,
                VehicleNumber = this.VehicleNumber
            };

            var listDbResultModels = this._dataAccessLayer.Search(dalSearchModel);

            // Update the View
            foreach (var dbSearchResultModel in listDbResultModels)
            {
                this.SearchDataList.Add(new SearchDataViewItem()
                {
                    ID = "Some id",
                    Date = dbSearchResultModel.EntryDateTime,
                    VehicleNumber = dbSearchResultModel.VehicleNumber
                });
            }
        }
    }

    public class SearchDataViewItem
    {
        private string _id;
        private DateTime _date;

        public ICommand ShowImage { get; set; }

        public SearchDataViewItem()
        {
            this.ShowImage = new RelayCommand(this.ShowImageCallback);
            this._id = "1";
            this._date = DateTime.Now;
        }
        
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string VehicleNumber
        {
            get; set;
        }

        public void ShowImageCallback(object args)
        {
            var camImagesViewWindow = new CameraImagesViewWindow();
            camImagesViewWindow.ShowDialog();
        }
    }
}
