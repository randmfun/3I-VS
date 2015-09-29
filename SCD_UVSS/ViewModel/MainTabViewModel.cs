using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.View;

namespace SCD_UVSS.ViewModel
{
    public class MainTabViewModel
    {
        private ObservableCollection<TabItem> _tabs = new ObservableCollection<TabItem>();
        
        public ObservableCollection<TabItem> Tabs
        {
            get { return this._tabs; }
            set { this._tabs = value; }
        }
        
        public MainTabViewModel()
        {
            Tabs = new ObservableCollection<TabItem>();

            this._tabs.Add(new TabItem { Header = "Camera View", ContentControl = new MainCameraView() });
            this._tabs.Add(new TabItem { Header = "Search", ContentControl = new SearchView()});
            this._tabs.Add(new TabItem { Header = "Gate Setup", ContentControl = new GateView() });
            this._tabs.Add(new TabItem { Header = "Black List", ContentControl = new BlackListView() });
        }

        public sealed class TabItem
        {
            public string Header { get; set; }

            public UserControl ContentControl { get; set; }
        }
    }
}
