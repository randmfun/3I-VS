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
            this.SearchDataList = new ObservableCollection<SearchDataItem>();
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

        public ObservableCollection<SearchDataItem> SearchDataList
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
                this.SearchDataList.Add(new SearchDataItem()
                {
                    ID = "Some id",
                    Date = dbSearchResultModel.EntryDateTime,
                    VehicleNumber = dbSearchResultModel.VehicleNumber
                });
            }
        }
    }

    public class SearchDataItem
    {
        public ICommand ShowImage { get; set; }

        public SearchDataItem()
        {
            this.ShowImage = new RelayCommand(this.ShowImageCallback);
            this.id = "1";
            this.date = DateTime.Now;
        }

        string id;
        DateTime date;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
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
