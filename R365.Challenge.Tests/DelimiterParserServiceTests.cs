﻿using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Models;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class DelimiterParserServiceTests
    {
        private IDelimiterParserService _delimiterParserService;

        [SetUp]
        public void Setup()
        {
            _delimiterParserService = new DelimiterParserService();
        }

        [Test]
        public void GetDelimiters_ShouldReturnOnlyDefaultDelimiters_WhenInputDoesNotContainCustomDelimiter()
        {
            var input = "5,tytyt";
            var calculation = _delimiterParserService.GetDelimiters(input);

            Assert.That(calculation.Delimiters, Is.Not.Empty);
            Assert.That(calculation.Delimiters.Count, Is.EqualTo(2));
            Assert.That(calculation.Delimiters.Contains(","), Is.True);
            Assert.That(calculation.Delimiters.Contains("\\n"), Is.True);
        }

        [Test]
        public void GetDelimiters_ShouldReturnSingleCharacterCustomAndDefaultDelimiters_WhenInputContainsSingleCharacterCustomDelimiter()
        {
            var input = "//#\\n2#5";
            var calculation = _delimiterParserService.GetDelimiters(input);

            Assert.That(calculation.Delimiters, Is.Not.Empty);
            Assert.That(calculation.Delimiters.Count, Is.EqualTo(3));
            Assert.That(calculation.Delimiters.Contains(","), Is.True);
            Assert.That(calculation.Delimiters.Contains("\\n"), Is.True);
            Assert.That(calculation.Delimiters.Contains("#"), Is.True);
        }

        [Test]
        public void GetDelimiters_ShouldReturnMultiCharacterCustomAndDefaultDelimiters_WhenInputContainsMultiCharacterCustomDelimiter()
        {
            var input = "//[***]\\n11***22***33";
            var calculation = _delimiterParserService.GetDelimiters(input);

            Assert.That(calculation.Delimiters, Is.Not.Empty);
            Assert.That(calculation.Delimiters.Count, Is.EqualTo(3));
            Assert.That(calculation.Delimiters.Contains(","), Is.True);
            Assert.That(calculation.Delimiters.Contains("\\n"), Is.True);
            Assert.That(calculation.Delimiters.Contains("***"), Is.True);
        }

        [Test]
        public void GetDelimiters_ShouldReturnAllMultiCharacterCustomAndDefaultDelimiters_WhenInputContainsArrayOfMultiCharacterCustomDelimiter()
        {
            var input = "//[*][!!][r9r]\\n11r9r22*hh*33!!44";
            var calculation = _delimiterParserService.GetDelimiters(input);

            Assert.That(calculation.Delimiters, Is.Not.Empty);
            Assert.That(calculation.Delimiters.Count, Is.EqualTo(5));
            Assert.That(calculation.Delimiters.Contains(","), Is.True);
            Assert.That(calculation.Delimiters.Contains("\\n"), Is.True);
            Assert.That(calculation.Delimiters.Contains("*"), Is.True);
            Assert.That(calculation.Delimiters.Contains("!!"), Is.True);
            Assert.That(calculation.Delimiters.Contains("r9r"), Is.True);
        }

        [Test]
        public void GetDelimiters_ShoudThrowException_WhenInputContainsIncorrectlyFormattedCustomDelimiter()
        {
            var input = "//#n2#5";
            Assert.Throws<ArgumentException>(() => _delimiterParserService.GetDelimiters(input));
        }
    }
}
