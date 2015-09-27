using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.Model;
using SCD_UVSS.ViewModel;

namespace UnitTest.ViewModel
{
    public class TestBlackListViewModel
    {
        [Test]
        public void TestAddBlacklistItem()
        {
            var mockDBProvider = new MockDatabaseProvider();
            var blackListItemViewModel = new BlackListViewModel(new DataAccessLayer(mockDBProvider))
            {
                AddVehicleNumber = "blah hum"
            };
            blackListItemViewModel.CmdAddBlackListedItem.Execute(null);
            Assert.AreEqual(mockDBProvider.BlackListItems.Count, 1);
        }

        [Test]
        public void TestAddEmptyBlacklistItem()
        {
            var mockDBProvider = new MockDatabaseProvider();
            var blackListItemViewModel = new BlackListViewModel(new DataAccessLayer(mockDBProvider))
            {
                AddVehicleNumber = string.Empty
            };
            blackListItemViewModel.CmdAddBlackListedItem.Execute(null);
            Assert.AreEqual(mockDBProvider.BlackListItems.Count, 0);
        }

        [Test]
        public void TestSearchAllBlackListItems()
        {
            var blackListItemViewModel = this.CreateSearchDblackListViewModel();
            blackListItemViewModel.SearchVehicleNumber = string.Empty;
            blackListItemViewModel.CmdSearchVehicleNumber.Execute(null);
            Assert.AreEqual(blackListItemViewModel.SearchResults.Count, 2);
        }

        [Test]
        public void TestSearchASubStringInBlackListItems()
        {
            var blackListItemViewModel = this.CreateSearchDblackListViewModel();
            blackListItemViewModel.SearchVehicleNumber = "1234";
            blackListItemViewModel.CmdSearchVehicleNumber.Execute(null);
            Assert.AreEqual(blackListItemViewModel.SearchResults.Count, 1);
        }

        private BlackListViewModel CreateSearchDblackListViewModel()
        {
            var mockDBProvider = new MockDatabaseProvider();
            var list = new[] { "TN 00 0000", "TN 00 1234" };
            mockDBProvider.BlackListItems = (from s in list
                                             select new BlackListItem(s)).ToList();
            return new BlackListViewModel(new DataAccessLayer(mockDBProvider));
        }
    }
}
