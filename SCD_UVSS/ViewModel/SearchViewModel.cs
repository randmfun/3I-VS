using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SCD_UVSS.Dal;
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
            this.EnDateTime = DateTime.Now;

            this.VehicleNumber = "";
        }

        public TimeSpanViewModel StartTimeSpanViewModel { get; set; }
        public TimeSpanViewModel EndTimeSpanViewModel { get; set; }


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
            var vehicle_no = "TN 0J 07656";

            for (int i = 211; i < 222; i++)
            {
                this.SearchDataList.Add(new SearchDataItem()
                {
                    ID = i.ToString(),
                    Date = DateTime.Now,
                    VehicleNumber = vehicle_no
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
