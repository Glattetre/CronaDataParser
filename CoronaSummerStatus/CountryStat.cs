using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoronaSummerStatus
{
    public class CountryStat
    {

        [JsonProperty("continent")]
        public string Continent;

        [JsonProperty("location")]
        public string Location;

        [JsonProperty("population")]
        public double Population;

        [JsonProperty("population_density")]
        public double PopulationDensity;

        [JsonProperty("median_age")]
        public double MedianAge;

        [JsonProperty("aged_65_older")]
        public double Aged65Older;

        [JsonProperty("aged_70_older")]
        public double Aged70Older;

        [JsonProperty("gdp_per_capita")]
        public double GdpPerCapita;

        [JsonProperty("cvd_death_rate")]
        public double CvdDeathRate;

        [JsonProperty("diabetes_prevalence")]
        public double DiabetesPrevalence;

        [JsonProperty("handwashing_facilities")]
        public double HandwashingFacilities;

        [JsonProperty("hospital_beds_per_thousand")]
        public double HospitalBedsPerThousand;

        [JsonProperty("life_expectancy")]
        public double LifeExpectancy;

        [JsonProperty("data")]
        public List<Datum> Data;

    }
}
