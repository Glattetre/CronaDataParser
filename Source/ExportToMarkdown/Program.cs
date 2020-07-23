using Flurl.Http;
using Glattetre.Covid19Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExportToMarkdown
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("New covid-19 per 100k and week");
            var source = "https://covid.ourworldindata.org/data/owid-covid-data.json";
            var data = await source.GetStringAsync();

            var parser = new CronaDataParser(data);

            string folder = @"..\..\..\..\..\Results";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.md";

            string path = Path.Combine(folder, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                while (File.Exists(path))
                    System.Threading.Thread.Sleep(1);
            }


            using (var file = new StreamWriter(path))
            {

                file.WriteLine("# New covid-19 per 100k and week");
                file.WriteLine($"Updated {DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");

                file.WriteLine(@"Data Source: https://github.com/owid/covid-19-data/tree/master/public/data");
                file.WriteLine();

                file.WriteLine("# Per week");

                // Heading
                file.Write("|Country|");
                WithWeeks(w => file.Write($"Week {w}|"));
                file.WriteLine();
                // Table seperator
                file.Write("| --- |");
                WithWeeks(w => file.Write($" --: |"));
                file.WriteLine();

                var countries = new string[] { "NOR", "DNK", "SWE", "DEU", "CHE", "AUT", "ITA", "USA", "ESP", "GBR" };
                foreach (var countryIso3 in countries)
                {
                    var country = parser.GetCountry(countryIso3);

                    file.Write($"|{country.Location}|");
                    WithWeeks(w => file.Write($"{country.Data.GetDataForWeek(w).NewCasesPerWeekPer100K().MS()} |"));
                    file.WriteLine();

                }

                file.WriteLine();

                file.WriteLine("# Last days");
                file.WriteLine("Number as of two days ago, as last two days often miss data.  Data is average per day, multiplied by 7.");
                file.WriteLine();
                var date = DateTime.Now.AddDays(-2);
                file.WriteLine($"Up to {date.Year}-{date.Month}-{date.Day}");

                file.WriteLine();

                // Heading
                file.WriteLine("|Country|last 28 days|Last 14 days|Last 7 days|Last 3 days|");
                file.WriteLine("| --- | --: | --: | --: | --: |");
                foreach (var countryIso3 in countries)
                {
                    var country = parser.GetCountry(countryIso3);

                    var last28 = country.NewCasesPerWeekPer100K(28, 2).MS();
                    var last14 = country.NewCasesPerWeekPer100K(14, 2).MS();
                    var last7 = country.NewCasesPerWeekPer100K(7, 2).MS();
                    var last3 = country.NewCasesPerWeekPer100K(3, 2).MS();

                    file.WriteLine($"|{country.Location}|{last28}|{last14}|{last7}|{last3}|");
                }

            }
        }
        static void WithWeeks(Action<int> withWeek)
        {
            var thisWeek = NorWeekHelper.GetWeekNumber();
            for (var weekNumber = thisWeek - 5; weekNumber <= thisWeek; weekNumber++)
            {
                withWeek(weekNumber);
            }
        }



    }

    static class Extensions
    {
        public static string MS(this double number)
        {
            var strNumber = number.ToString("0.00");
            if (number >= 10)
            {
                strNumber = $"**{strNumber}**";
            }
            return strNumber;
        }
    }
}
