using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SCD_UVSS.Dal;

namespace SCD_UVSS.ViewModel
{
    public class SearchViewModel
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public SearchViewModel(DataAccessLayer dataAccessLayer):this()
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
            this.EnDateTime = DateTime.Now;

            this.VehicleNumber = "some number";
        }

        public TimeSpanViewModel StartTimeSpanViewModel { get; set; }
        public TimeSpanViewModel EndTimeSpanViewModel { get; set; }

        public ICommand SearchCommand { get; set; }

        public string VehicleNumber { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EnDateTime
        {
            get; set;
        }

       public ObservableCollection<SearchDataItem> SearchDataList
        {
            get; set;
        }
            


        public void Search(object arg)
        {
            this.SearchDataList.Add(new SearchDataItem() { ID = "10", Date = DateTime.Now });
            this.SearchDataList.Add(new SearchDataItem() { ID = "10", Date = DateTime.Now });
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

        public void ShowImageCallback(object args)
        {
            MessageBox.Show("Image Window would come up for" + ID);
        }
    }
}
