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
        public ObservableCollection<TabItem> Tabs { get; set; }

        public MainTabViewModel()
        {
            Tabs = new ObservableCollection<TabItem>();

            var cameraViewsCtrl = new CameraViews();
            cameraViewsCtrl.DataContext = new CameraListViewModel();

            var gateViewCtrl = new GateView();
            gateViewCtrl.DataContext = new GateSetupViewModel(new DataAccessLayer(new MySqlDatabaseProvider()));

            Tabs.Add(new TabItem {Header = "Camera View", Content = cameraViewsCtrl});
            Tabs.Add(new TabItem { Header = "Gate Setup", Content = gateViewCtrl});
        }

        public sealed class TabItem
        {
            public string Header { get; set; }

            public UserControl Content { get; set; }
        }
    }
}
