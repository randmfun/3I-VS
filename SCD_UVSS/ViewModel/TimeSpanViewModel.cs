using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SCD_UVSS.ViewModel
{
    public class TimeSpanViewModel
    {
        public IEnumerable<int> Hours
        {
            get; set;
        }

        public IEnumerable<int> Minutes
        {
            get; set;
        }

        public int SelectedHour { get; set; }

        public int SelectedMinute { get; set; }

        public TimeSpan SelectedTimeSpan
        {
            get { return new TimeSpan(0, this.SelectedHour, this.SelectedMinute, 0); }
        }

        public TimeSpanViewModel()
        {
            this.Hours = Enumerable.Range(0, 24);
            this.Minutes = Enumerable.Range(0, 60);
        }
    }
}
