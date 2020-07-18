using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaSummerStatus
{
    public class CronaDataParser
    {

        Dictionary<string, CountryStat> _countries;

        public CronaDataParser(string json)
        {

            dynamic dynamicData = JsonConvert.DeserializeObject(json);
            JObject jObject = dynamicData;
            _countries = jObject.ToObject<Dictionary<string, CountryStat>>();
        }

        public CountryStat GetCountry(string iso3)
        {
            return _countries[iso3];
        }
    }
}
