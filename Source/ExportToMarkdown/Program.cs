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
                    WithWeeks(w => file.Write($"{country.Data.GetDataForWeek(w).NewCasesPerWeekPer100K().ToString("0.00")} |"));
                    file.WriteLine();

                }

                file.WriteLine();

                file.WriteLine("# Last days");

                // Heading
                file.WriteLine("|Country|last 28 days|Last 14 days|Last 7 days|Last 3 days|Last 3, 2 days ago|");
                file.WriteLine("| --- | --: | --: | --: | --: || --: |");
                foreach (var countryIso3 in countries)
                {
                    var country = parser.GetCountry(countryIso3);

                    var last28 = country.NewCasesPerWeekPer100K(28).ToString("0.00");
                    var last14 = country.NewCasesPerWeekPer100K(14).ToString("0.00");
                    var last7 = country.NewCasesPerWeekPer100K(7).ToString("0.00");
                    var last3 = country.NewCasesPerWeekPer100K(3).ToString("0.00");
                    var last3ago2 = country.NewCasesPerWeekPer100K(3,2).ToString("0.00");

                    file.WriteLine($"|{country.Location}|{last28}|{last14}|{last7}|{last3}|{last3ago2}|");
                }

            }

            static void WithWeeks(Action<int> withWeek)
            {
                var thisWeek = NorWeekHelper.GetWeekNumber();
                for (var weekNumber = thisWeek - 10; weekNumber <= thisWeek; weekNumber++)
                {
                    withWeek(weekNumber);
                }
            }
        }
    }
}
