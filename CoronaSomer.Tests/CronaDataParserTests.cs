using NUnit.Framework;
using System.IO;
using CoronaSummerStatus;
using System;
using System.Linq;
using Glattetre.Covid19Data;

namespace CoronaSomer.Tests
{
    public class CronaDataParserTests
    {
        CronaDataParser _parser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var jsonText = File.ReadAllText("coronadata.json");
            _parser = new CronaDataParser(jsonText);
        }

        [Test]
        public void CronaDataParserTests_LoadCountry_Found([Values("NOR", "DNK", "SWE", "DEU", "CHE", "AUT", "ITA", "USA")]string iso3)
        {
            // Set up

            // Action
            var country = _parser.GetCountry(iso3);
            // Act

            Assert.That(country, Is.Not.Null);
        }

        [Test]
        public void CronaDataParserTests_GetLatestData_Last14Days([Values("NOR", "DNK", "SWE", "DEU", "CHE", "AUT", "ITA", "USA")] string iso3)
        {
            // Set up
            var country = _parser.GetCountry(iso3);

            var allowedTodayDates = new DateTime[] { new DateTime(2020, 07, 18), new DateTime(2020, 07, 17) };
            var allowedTwoWeeksAgoDates = new DateTime[] { new DateTime(2020, 07, 4), new DateTime(2020, 07, 4), new DateTime(2020, 07, 3) };

            // Action
            var data = country.GetLatestData();

            // Act
            Assert.That(data, Has.Length.EqualTo(14));
            Assert.That(data.First().Date, Is.EqualTo(new DateTime(2020, 07, 17)).Within(TimeSpan.FromDays(1)));
            Assert.That(data.Last().Date, Is.EqualTo(new DateTime(2020, 07, 4)).Within(TimeSpan.FromDays(1)));
        }



        [Test]
        public void CronaDataParserTests_Average100K_AsExpectedAccordingToFHI([Values("NOR", "DNK", "SWE", "DEU", "CHE", "AUT", "ITA", "USA")] string iso3)
        {
            // Set up
            var country = _parser.GetCountry(iso3);

            // act
            var data = country.GetLatestData();
            var average = data.NewCasesPerWeekPer100K();
            var average2 = country.NewCasesPerWeekPer100K();

            Assert.That(average, Is.EqualTo(average2));
            // assert
            switch (iso3)
            {
                case "NOR":
                    Assert.That(average, Is.GreaterThan(0));
                    Assert.That(average, Is.LessThan(2));
                    break;
                case "SWE":
                case "USA":
                    Assert.That(average, Is.GreaterThan(20));
                    Assert.That(average, Is.LessThan(200));
                    break;
                default:
                    Assert.That(average, Is.GreaterThan(1));
                    Assert.That(average, Is.LessThan(20));
                    break;
            }
        }
    }
}