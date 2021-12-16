using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalUtil
{
    public static class DateatimeExtentions
    {
        public static DateTime RoundToHour(this DateTime value)
        {
            DateTime updated = value.AddMinutes(30);
            return new DateTime(updated.Year, updated.Month, updated.Day, updated.Hour, 0, 0, 0);
        }

        public static bool IsEmpty(this DateTime value)
        {
            return value == default(DateTime);
        }
    }
}
