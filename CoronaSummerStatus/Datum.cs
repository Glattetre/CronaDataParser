using Newtonsoft.Json;
using System;

namespace CoronaSummerStatus
{
    public class Datum
    {

        [JsonProperty("date")]
        public DateTime Date;

        [JsonProperty("total_cases")]
        public double TotalCases;

        [JsonProperty("new_cases")]
        public double NewCases;

        [JsonProperty("total_deaths")]
        public double TotalDeaths;

        [JsonProperty("new_deaths")]
        public double NewDeaths;

        [JsonProperty("total_cases_per_million")]
        public double TotalCasesPerMillion;

        [JsonProperty("new_cases_per_million")]
        public double NewCasesPerMillion;

        [JsonProperty("total_deaths_per_million")]
        public double TotalDeathsPerMillion;

        [JsonProperty("new_deaths_per_million")]
        public double NewDeathsPerMillion;

        [JsonProperty("stringency_index")]
        public double? StringencyIndex;

    }
}
