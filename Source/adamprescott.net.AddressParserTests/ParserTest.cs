using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adamprescott.net.AddressParser;

namespace adamprescott.net.AddressParserTests
{
    [TestClass]
    public class ParserTest
    {
        private void CompareAddresses(Address expected, Address actual)
        {
            Assert.IsNotNull(actual);
            CompareStrings(expected.HouseNumber, actual.HouseNumber, "HouseNumber");
            CompareStrings(expected.StreetName, actual.StreetName, "StreetName");
            CompareStrings(expected.StreetPrefix, actual.StreetPrefix, "StreetPrefix");
            CompareStrings(expected.StreetSuffix, actual.StreetSuffix, "StreetSuffix");
            CompareStrings(expected.StreetType, actual.StreetType, "StreetType");
            CompareStrings(expected.Apt, actual.Apt, "Apt");
        }

        private void CompareStrings(string expected, string actual, string message)
        {
            if (string.IsNullOrEmpty(expected) && string.IsNullOrEmpty(actual))
            {
                return;
            }
            Assert.AreEqual(expected, actual, message);
        }

        [TestMethod]
        public void ParseReturnsEmptyAddressForNullInput()
        {
            // Arrange
            var expected = new Address();

            // Act
            var actual = Parser.Parse(null);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseParsesHouseNumberAndStreetName()
        {
            // Arrange
            const string input = "100 MAIN";
            var expected = new Address
            {
                HouseNumber = "100",
                StreetName = "MAIN"
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseParsesHouseNumberStreetNameAndStreetType()
        {
            // Arrange
            const string input = "100 MAIN ST";
            var expected = new Address
            {
                HouseNumber = "100",
                StreetName = "MAIN",
                StreetType = "ST"
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseParsesHouseNumberStreetPrefixStreetNameAndStreetType()
        {
            // Arrange
            const string input = "100 S MAIN ST";
            var expected = new Address
            {
                HouseNumber = "100",
                StreetName = "MAIN",
                StreetPrefix = "S",
                StreetType = "ST"
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseParsesHouseNumberStreetPrefixStreetNameStreetTypeAndStreetSuffix()
        {
            // Arrange
            const string input = "100 S MAIN ST W";
            var expected = new Address
            {
                HouseNumber = "100",
                StreetName = "MAIN",
                StreetPrefix = "S",
                StreetSuffix = "W",
                StreetType = "ST"
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseParsesHouseNumberStreetPrefixStreetNameStreetTypeStreetSuffixAndApartment()
        {
            // Arrange
            const string input = "100 S MAIN ST W APT 1A";
            var expected = new Address
            {
                HouseNumber = "100",
                StreetName = "MAIN",
                StreetPrefix = "S",
                StreetSuffix = "W",
                StreetType = "ST",
                Apt = "APT 1A"
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseResultsAreAllUppercase()
        {
            // Arrange
            const string input = "100 s main st w apt 1a";
            var expected = new Address
            {
                HouseNumber = "100",
                StreetName = "MAIN",
                StreetPrefix = "S",
                StreetSuffix = "W",
                StreetType = "ST",
                Apt = "APT 1A"
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }

        [TestMethod]
        public void ParseAssignsInputToStreetNameWhenInputCannotBeParsed()
        {
            // Arrange
            const string input = "127.0.0.1 IS NOT A VALID FORMAT";
            var expected = new Address
            {
                StreetName = input
            };

            // Act
            var actual = Parser.Parse(input);

            // Assert
            CompareAddresses(expected, actual);
        }
    }
}
