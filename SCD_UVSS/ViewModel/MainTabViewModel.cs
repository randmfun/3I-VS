using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Castle.Windsor;
using Castle.Windsor.Installer;
using SCD_UVSS.Controller;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Registers;
using SCD_UVSS.View;

namespace SCD_UVSS.ViewModel
{
    public class MainTabViewModel
    {
        private IWindsorContainer container;

        //private RecordManager _recordManager;

        private ObservableCollection<TabItem> _tabs = new ObservableCollection<TabItem>();
        
        public ObservableCollection<TabItem> Tabs
        {
            get { return this._tabs; }
            set { this._tabs = value; }
        }
        
        public MainTabViewModel()
        {
            container = new WindsorContainer();
            container.Install(FromAssembly.Containing<Installers>());

            Tabs = new ObservableCollection<TabItem>();

            this._tabs.Add(new TabItem { Header = "Camera View", ContentControl = container.Resolve<MainCameraView>() });
            this._tabs.Add(new TabItem { Header = "Search", ContentControl = container.Resolve<SearchView>()});
            this._tabs.Add(new TabItem { Header = "Gate Setup", ContentControl = container.Resolve<GateView>()});
            this._tabs.Add(new TabItem { Header = "Black List", ContentControl = container.Resolve<BlackListView>()});
        }
        
        public sealed class TabItem
        {
            public string Header { get; set; }

            public UserControl ContentControl { get; set; }
        }
    }
}
