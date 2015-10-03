using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCD_UVSS.helpers
{
    public static class DateTimeSyntaticSugars
    {
        public static string ConvertToMySqlFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd H:mm:ss");
        }

        public static string GetDatePortion(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string GetTimePortion(this DateTime dateTime)
        {
            return dateTime.ToString("H:mm:ss");
        }

    }
}
