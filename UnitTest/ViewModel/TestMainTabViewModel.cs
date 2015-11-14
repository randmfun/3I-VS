using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Model;
using SCD_UVSS.ViewModel;

namespace UnitTest.ViewModel
{
    public class TestMainTabViewModel
    {
        [Test]
        public void TestTabsConstructForDeveloperMode()
        {
            var mainTabViewModel = new MainTabViewModel(new UserInfo(){Role = Roles.Developer});
            var tabs = mainTabViewModel.Tabs;

            Assert.AreEqual(tabs.Count, 5);
        }

        [Test]
        public void TestTabsConstructForAdminMode()
        {
            var mainTabViewModel = new MainTabViewModel(new UserInfo() { Role = Roles.Admin });
            var tabs = mainTabViewModel.Tabs;

            Assert.AreEqual(tabs.Count, 4);
        }

        [Test]
        public void TestTabsConstructForOperatorMode()
        {
            var mainTabViewModel = new MainTabViewModel(new UserInfo() { Role = Roles.Operator });
            var tabs = mainTabViewModel.Tabs;

            Assert.AreEqual(tabs.Count, 2);
        }
    }
}
