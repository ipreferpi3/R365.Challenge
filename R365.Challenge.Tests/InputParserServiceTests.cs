using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class InputParserServiceTests
    {
        private IInputParserService _inputParserService;

        [SetUp]
        public void Setup()
        {
            _inputParserService = new InputParserService();
        }

        [Test]
        public void Parse_ShouldReturnListOfInts_WhenGivenArrayOfStringsContainingNumbers()
        {
            var input = new string[] { "1", "2", "3" };

            var result = _inputParserService.Parse(input);

            Assert.That(result, Is.EqualTo(new List<int> { 1, 2, 3 }));
        }

        [Test]
        public void Parse_ShouldReturnListOfInts_WhenGivenArrayOfStringsContainingNumbersAndNonNumbers()
        {
            var input = new string[] { "1", "b", "3", "d" };

            var result = _inputParserService.Parse(input);

            Assert.That(result, Is.EqualTo(new List<int> { 1, 0, 3, 0 }));
        }

        [Test]
        public void Parse_ShouldReturnListOfInts_WhenGivenArrayOfStringsContainingNonNumbers()
        {
            var input = new string[] { "a", "b", "c" };

            var result = _inputParserService.Parse(input);

            Assert.That(result, Is.EqualTo(new List<int> { 0, 0, 0 }));
        }

        [Test]
        public void Parse_ShouldReturnIntsGreaterThan1000_AsValue0_WhenCeilingIsNotExplicitlyPassedToFunction()
        {
            var input = new string[] { "a", "2", "1000", "1001" };

            var result = _inputParserService.Parse(input);

            Assert.That(result, Is.EqualTo(new List<int> { 0, 2, 1000, 0 }));
        }

        [Test]
        public void Parse_ShouldReturnIntsGreaterThanCeiling_AsValue0_WhenCeilingIsProvided()
        {
            var input = new string[] { "a", "2", "1000", "1001" };

            var result = _inputParserService.Parse(input, ceiling: 500);

            Assert.That(result, Is.EqualTo(new List<int> { 0, 2, 0, 0 }));
        }

        [Test]
        public void Parse_ShouldThrowArguementException_WhenGivenArrayOfStringsContainingNegatives_AndAllowNegativesIsFalse()
        {
            var input = new string[] { "1", "b", "-4" };

            Assert.Throws<ArgumentException>(() => _inputParserService.Parse(input, allowNegatives: false));
        }

        [Test]
        public void Parse_ShouldThrowArguementException_WhenGivenArrayOfStringsContainingNegatives_AndAllowNegativesIsNotProvided()
        {
            var input = new string[] { "1", "b", "-4" };

            Assert.Throws<ArgumentException>(() => _inputParserService.Parse(input));
        }

        [Test]
        public void Parse_ShouldAcceptValues_WhenGivenArrayOfStringContainingNegatives_AndAllowNegativesIsTrue()
        {
            var input = new string[] { "1", "b", "-4" };

            var result = _inputParserService.Parse(input, allowNegatives: true);

            Assert.That(result, Is.EqualTo(new List<int> { 1, 0, -4 }));
        }
    }
}
