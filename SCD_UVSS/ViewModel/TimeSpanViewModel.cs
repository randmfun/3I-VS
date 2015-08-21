using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SCD_UVSS.ViewModel
{
    public class TimeSpanViewModel
    {
        public IEnumerable<string> Hours
        {
            get; set;
        }

        public IEnumerable<string> Minutes
        {
            get; set;
        }

        public IEnumerable<string> Sessions { get; set; } 

        public string SelectedHour { get; set; }

        public string SelectedMinute { get; set; }

        public string SelectedSession { get; set; }

        public TimeSpan SelectedTimeSpan
        {
            get { return new TimeSpan(0, int.Parse(this.SelectedHour), int.Parse(this.SelectedMinute), 0); }
        }

        public TimeSpanViewModel()
        {
            this.Hours = Enumerable.Range(0, 12).Select(x => string.Format("{0:00}", x));
            this.Minutes = new List<string> { "00", "15", "30", "45" };
            this.Sessions = new List<string> {"AM", "PM"};

            this.SelectedHour = this.Hours.First();
            this.SelectedMinute = this.Minutes.First();
            this.SelectedSession = this.Sessions.First();
        }
    }
}
