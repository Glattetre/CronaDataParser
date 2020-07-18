using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CoronaSummerStatus
{
    public static class Extensions
    {
        public static Datum[] GetLatestData(this CountryStat stat, int days = 14, int skip = 0)
        {

            return stat.Data.OrderByDescending(d => d.Date).Skip(skip).Take(days).ToArray();
        
        }

        public static double NewCasesPerWeekPer100K(this Datum[] data)
        {
            return data.Average(d => d.NewCasesPerMillion * 7 / 10);
        }
        public static double NewCasesPerWeekPer100K(this CountryStat stat, int days = 14, int skip = 0)
        {
            return stat.GetLatestData(days, skip).NewCasesPerWeekPer100K();
        }

        public static double Average(this Datum[] data, Func<Datum, double> getValue)
        {

            return data.Select(d => getValue(d)).Average();        
        }

    }
}
