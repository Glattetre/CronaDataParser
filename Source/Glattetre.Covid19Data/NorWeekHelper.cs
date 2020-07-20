using System;
using System.Collections.Generic;
using System.Text;

namespace Glattetre.Covid19Data
{
    public class NorWeekHelper
    {
        public static DateTime GetMondayOfWeek1()
        {
            var first = new DateTime(DateTime.Now.Year, 1, 1);
            switch (first.DayOfWeek)
            {
                default:
                case DayOfWeek.Monday: return first;
                case DayOfWeek.Tuesday: return first.AddDays(-1);
                case DayOfWeek.Wednesday: return first.AddDays(-2);
                case DayOfWeek.Thursday: return first.AddDays(-3);
                case DayOfWeek.Friday: return first.AddDays(+3);
                case DayOfWeek.Saturday: return first.AddDays(2);
                case DayOfWeek.Sunday: return first.AddDays(+1);
            }
        }
        public static int GetWeekNumber()
        {
            var daysSinceMondayWeekOne = (int)(DateTime.Now - GetMondayOfWeek1()).TotalDays;
            var weekNumber = ((int)daysSinceMondayWeekOne / 7) + 1;
            return weekNumber;
        }
    }
}
