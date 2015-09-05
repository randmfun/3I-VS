using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.ViewModel;

namespace UnitTest.ViewModel
{
    public class TestSearchViewModel
    {
        [Test]
        public void SearchViewModelInit()
        {
            var searchViewModel = new SearchViewModel(null);

            Assert.AreEqual(searchViewModel.StartTimeSpanViewModel.SelectedHour, "00");
            Assert.AreEqual(searchViewModel.StartTimeSpanViewModel.SelectedMinute, "00");
            Assert.AreEqual(searchViewModel.StartTimeSpanViewModel.SelectedSession, "AM");
            
            Assert.AreEqual(searchViewModel.StartDateTime.Hour, DateTime.Now.Hour);
            Assert.AreEqual(searchViewModel.EndDateTime.Hour, DateTime.Now.Hour);
            Assert.NotNull(searchViewModel.SearchDataList);
        }

        [Test]
        public void SearchDataItem()
        {
            var searchViewModel = new SearchViewModel(null);
            searchViewModel.SearchDataList.Add(new SearchDataViewItem() { ID = "10", Date = DateTime.Now });

            Assert.AreEqual(searchViewModel.SearchDataList.First().ID, "10");
        }
    }
}
