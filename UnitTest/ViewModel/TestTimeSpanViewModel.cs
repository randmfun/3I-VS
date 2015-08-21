using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.ViewModel;

namespace UnitTest.ViewModel
{
    public class TestTimeSpanViewModel
    {
        [Test]
        public void TimeSpanViewModelInit()
        {
            var timeSpanViewModel = new TimeSpanViewModel();

            Assert.AreEqual(timeSpanViewModel.Hours.Count(), 12);
            Assert.AreEqual(timeSpanViewModel.Minutes.Count(), 4);
            Assert.AreEqual(timeSpanViewModel.Sessions.Count(), 2);

            Assert.AreEqual(timeSpanViewModel.SelectedHour, "00");
            Assert.AreEqual(timeSpanViewModel.SelectedMinute, "00");
            Assert.AreEqual(timeSpanViewModel.SelectedSession, "AM");

            Assert.AreEqual(timeSpanViewModel.SelectedTimeSpan.Hours, int.Parse(timeSpanViewModel.SelectedHour));
        }
    }
}
