using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SCD_UVSS.ViewModel
{
    public class SearchViewModel
    {
        public ICommand SearchCommand { get; set; }

        public string VehicleNumber { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EnDateTime
        {
            get; set;
        }

       public ObservableCollection<SearchData> SearchDataList
        {
            get; set;
        }
            
        public SearchViewModel()
        {
            this.SearchDataList = new ObservableCollection<SearchData>();

            this.SearchCommand = new RelayCommand(Search);
            this.StartDateTime = DateTime.Now;
            this.EnDateTime = DateTime.Now;

            this.VehicleNumber = "some number";

            this.SearchDataList.Add(new SearchData() {ID = "10", Date = DateTime.Now});
            this.SearchDataList.Add(new SearchData() { ID = "10", Date = DateTime.Now });
        }

        public void Search(object arg)
        {
            
        }
    }

    public class SearchData
    {
        public SearchData()
        {
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
    }
}
