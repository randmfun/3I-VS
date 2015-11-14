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
using SCD_UVSS.Dal.UserProvider;
using SCD_UVSS.Model;
using SCD_UVSS.Registers;
using SCD_UVSS.View;

namespace SCD_UVSS.ViewModel
{
    public class MainTabViewModel
    {
        private readonly IWindsorContainer _container;
        private UserInfo _loggedInUserInfo;

        public UserInfo LoggedInUser
        {
            get { return _loggedInUserInfo ?? (_loggedInUserInfo = UserManager.Instance.LoggedInUser); }
        }

        private ObservableCollection<TabItem> _tabs = new ObservableCollection<TabItem>();
        
        public ObservableCollection<TabItem> Tabs
        {
            get { return this._tabs; }
            set { this._tabs = value; }
        }

        public MainTabViewModel(UserInfo loggedInuserInfo)
        {
            this._loggedInUserInfo = loggedInuserInfo;
            
            _container = new WindsorContainer();
            _container.Install(FromAssembly.Containing<Installers>());

            this.LoadValidTabsBasedOnUser();
        }

        public MainTabViewModel():this(null)
        {

        }

        private void LoadValidTabsBasedOnUser()
        {
            Tabs = new ObservableCollection<TabItem>();

            switch (this.LoggedInUser.Role)
            {
                case Roles.Operator:
                    this._tabs.Add(new TabItem { Header = "Camera View", ContentControl = _container.Resolve<MainCameraView>() });
                    this._tabs.Add(new TabItem { Header = "Search", ContentControl = _container.Resolve<SearchView>() });
                    break;

                case Roles.Admin:
                    this._tabs.Add(new TabItem { Header = "Camera View", ContentControl = _container.Resolve<MainCameraView>() });
                    this._tabs.Add(new TabItem { Header = "Search", ContentControl = _container.Resolve<SearchView>() });

                    this._tabs.Add(new TabItem { Header = "Black List", ContentControl = _container.Resolve<BlackListView>() });
                    this._tabs.Add(new TabItem {Header = "Edit Users", ContentControl = _container.Resolve<UserManagerView>()});
                    break;
                
                case Roles.Developer:
                    this._tabs.Add(new TabItem { Header = "Camera View", ContentControl = _container.Resolve<MainCameraView>() });
                    this._tabs.Add(new TabItem { Header = "Search", ContentControl = _container.Resolve<SearchView>() });

                    this._tabs.Add(new TabItem { Header = "Black List", ContentControl = _container.Resolve<BlackListView>() });
                    this._tabs.Add(new TabItem {Header = "Edit Users", ContentControl = _container.Resolve<UserManagerView>()});
                    
                    this._tabs.Add(new TabItem { Header = "Gate Setup", ContentControl = _container.Resolve<GateView>() });
                    break;
            }
        }
        
        public sealed class TabItem
        {
            public string Header { get; set; }

            public UserControl ContentControl { get; set; }
        }
    }
}
