using Flurl.Http;
using Glattetre.Covid19Data;
using System;
using System.Threading.Tasks;

namespace CoronaSummerStatus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("New covid-19 per 100k and week");
            var source = "https://covid.ourworldindata.org/data/owid-covid-data.json";
            var data = await source.GetStringAsync();

            var parser = new CronaDataParser(data);

            Console.WriteLine("Country;28 days;14 days;5 days");

            //"NOR", "DNK", "SWE", "DEU", "CHE", "AUT", "ITA", "USA"
            WriteCountry(parser.GetCountry("NOR"));
            WriteCountry(parser.GetCountry("DNK"));
            WriteCountry(parser.GetCountry("DEU"));
            WriteCountry(parser.GetCountry("CHE"));
            WriteCountry(parser.GetCountry("ITA"));
            WriteCountry(parser.GetCountry("AUT"));
            WriteCountry(parser.GetCountry("SWE"));
            WriteCountry(parser.GetCountry("USA"));
            WriteCountry(parser.GetCountry("ESP"));
            WriteCountry(parser.GetCountry("GBR"));

            Console.WriteLine($"Data from: {source}");
            Console.WriteLine("Average of per milion/10 and multiplied by 7 (one week)");

        }

        static void WriteCountry(CountryStat country)
        {

            var data28 = country.NewCasesPerWeekPer100K(8).ToString("0.00");
            var data14 = country.NewCasesPerWeekPer100K(14).ToString("0.00");
            var data5 = country.NewCasesPerWeekPer100K(5).ToString("0.00");
            Console.WriteLine($"{country.Location};{data28};{data14};{data5}");
        
        }


    }
}
