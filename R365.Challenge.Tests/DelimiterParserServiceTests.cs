using FakeItEasy;
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
            Assert.That(calculation.Delimiters.Contains("\n"), Is.True);
        }

        [Test]
        public void GetDelimiters_ShouldReturnCustomAndDefaultDelimiters_WhenInputContainsCustomDelimiter()
        {
            var input = "//#\n2#5";
            var calculation = _delimiterParserService.GetDelimiters(input);

            Assert.That(calculation.Delimiters, Is.Not.Empty);
            Assert.That(calculation.Delimiters.Count, Is.EqualTo(3));
            Assert.That(calculation.Delimiters.Contains(","), Is.True);
            Assert.That(calculation.Delimiters.Contains("\n"), Is.True);
            Assert.That(calculation.Delimiters.Contains("#"), Is.True);
        }

        [Test]
        public void GetDelimiters_ShoudThrowException_WhenInputContainsIncorrectlyFormattedCustomDelimiter()
        {
            var input = "//#n2#5";
            Assert.Throws<ArgumentException>(() =>  _delimiterParserService.GetDelimiters(input));
        }
    }
}
