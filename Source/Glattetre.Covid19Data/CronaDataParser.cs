using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Glattetre.Covid19Data
{
    public class CronaDataParser : IEnumerable<KeyValuePair<string,CountryStat>>
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

        public IEnumerator<KeyValuePair<string, CountryStat>> GetEnumerator()
        {
            return _countries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _countries.GetEnumerator();
        }
    }
}
