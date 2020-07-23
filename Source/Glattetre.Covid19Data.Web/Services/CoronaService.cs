using Glattetre.Covid19Data.Web.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using System.Threading;

namespace Glattetre.Covid19Data.Web.Services
{
    public interface ICoronaService
    {
        Task<CountryListItemModel[]> GetCountryList();
    }


    public class CoronaService : ICoronaService
    {
        readonly HttpClient _httpClient;
        ILogger<CoronaService> _logger;

        CronaDataParser _parser;
        DateTime _loadTime;
        static readonly TimeSpan _expiry = TimeSpan.FromMinutes(60);
        const string Source = "https://covid.ourworldindata.org/data/owid-covid-data.json";
        static SemaphoreSlim _parserLoadSempahore = new SemaphoreSlim(1, 1);

        public CoronaService(HttpClient httpClient, ILogger<CoronaService> logger)
        {
            _httpClient = httpClient;
        }

        async Task<CronaDataParser> GetParserAsync()
        {
            await _parserLoadSempahore.WaitAsync();
            try
            {
                if (_parser != null && (_loadTime + _expiry) > DateTime.UtcNow)
                {
                    return _parser;
                }

                var data = await Source.GetStringAsync();
                _parser = new CronaDataParser(data);
                _loadTime = DateTime.UtcNow;

                return _parser;
            }
            finally
            {
                _parserLoadSempahore.Release();
            }
        }

        public async Task<CountryListItemModel[]> GetCountryList()
        {
            var parser = await GetParserAsync();
            var retVal = parser.Select(c => new CountryListItemModel 
            {
                Code = c.
            }).ToArray();
            return retVal;
        }
    }
}
