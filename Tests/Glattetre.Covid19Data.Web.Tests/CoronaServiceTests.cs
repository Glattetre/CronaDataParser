using Glattetre.Covid19Data.Web.Models;
using Glattetre.Covid19Data.Web.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Glattetre.Covid19Data.Web.Tests
{
    public class CoronaServiceTests
    {
        HttpClient _httpClient;
        ILogger<CoronaService> _logger;
        ICoronaService _coronaService;
        int _requestCounter;

        [SetUp]
        public void SetUp()
        {
            var jsonText = File.ReadAllText("coronadata.json");
            _requestCounter = 0;

            var messageHandler = new MockHttpMessageHandler(input =>
            {
                _requestCounter++;
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonText)
                };

            });
            _httpClient = new HttpClient(messageHandler);
            _logger = Substitute.For<ILogger<CoronaService>>();
            _coronaService = new CoronaService(_httpClient, _logger);
        }

        [Test]
        public void GetCountryList_Any_ContainsNorway()
        {

            // arrange
            // act
            var countryList = _coronaService.GetCountryList();
            // assert
            Assert.That(countryList, Has.Exactly(1).Items.Property(nameof(CountryListItemModel.Name)).EqualTo("Norway"));
        }
    }
}