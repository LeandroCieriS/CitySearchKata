using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NUnit.Framework;

namespace CitySearchKata
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            CitySearch.Database = new List<string>
            {
                "Paris", "Budapest", "Skopje", "Rotterdam", "Valencia", "Vancouver", "Amsterdam", "Vienna", "Sydney",
                "New York City", "London", "Bangkok", "Hong Kong", "Dubai", "Rome", "Istanbul"
            };
        }

        [Test]
        public void return_empty_if_input_is_less_than_2_chars()
        {
            var result = CitySearch.Execute("a");

            Assert.IsTrue(!result.Any());
        }

        [Test]
        public void return_empty_if_no_input()
        {
            var result = CitySearch.Execute("");

            Assert.IsTrue(!result.Any());
        }

        [Test]
        public void return_cities_starting_with_input()
        {
            var result = CitySearch.Execute("Va");

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains("Valencia"));
            Assert.IsTrue(result.Contains("Vancouver"));
        }
        [Test]
        public void be_case_insensitive()
        {
            var result = CitySearch.Execute("vA");

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains("Valencia"));
            Assert.IsTrue(result.Contains("Vancouver"));
        }

        [Test]
        public void return_city_if_contains_input()
        {
            var result = CitySearch.Execute("ape");

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Contains("Budapest"));
        }

        [Test]
        public void return_all_cities_if_input_is_asterisk()
        {
            var result = CitySearch.Execute("*");

            Assert.IsTrue(result.Count == 16);
            Assert.IsTrue(result.Equals(CitySearch.Database));
            Assert.IsFalse(ContainsDuplicates(result));
        }

        private static bool ContainsDuplicates(IEnumerable<string> result)
        {
            return result.GroupBy(x => x).Where(g => g.Count() > 1).ToList().Any();
        }
    }

    public class CitySearch
    {
        public static List<string> Database { get; set; }

        public static List<string> Execute(string input)
        {
            var result = new List<string>();
            if (input.Equals("*")) return Database;
            if (input.Length < 2) return result;
            result.AddRange(CollectionItemsContaining(input));
            return result;
        }

        private static IEnumerable<string> CollectionItemsContaining(string input)
        {
            return Database.Where(city => city.ToLower().Contains(input.ToLower()));
        }
    }


}