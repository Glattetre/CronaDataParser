using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glattetre.Covid19Data
{
    public static class Extensions
    {
        public static Datum[] GetLatestData(this CountryStat stat, int days = 14, int skip = 0)
        {

            return stat.Data.OrderByDescending(d => d.Date).Skip(skip).Take(days).ToArray();
        
        }

        public static double NewCasesPerWeekPer100K(this Datum[] data)
        {
            if (data == null || data.Length == 0)
                return 0;
            
            return data.Average(d => d.NewCasesPerMillion * 7 / 10);
        }
        public static double NewCasesPerWeekPer100K(this CountryStat stat, int days = 14, int skip = 0)
        {
            return stat.GetLatestData(days, skip).NewCasesPerWeekPer100K();
        }

        public static double Average(this IEnumerable<Datum> data, Func<Datum, double> getValue)
        {

            return data.Select(d => getValue(d)).Average();        
        }

        public static Datum[] GetDataForWeek(this IEnumerable<Datum> data, int weekNumber)
        {
            var first = NorWeekHelper.GetMondayOfWeek1().AddDays((weekNumber - 1) * 7);
            var next = first.AddDays(7);

            return data.Where(d => d.Date >= first && d.Date < next)
                .OrderByDescending(d => d.Date)
                .ToArray();
            
        }

        

    }
}
