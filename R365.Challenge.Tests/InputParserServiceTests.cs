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
        public void Parse_ShouldThrowArguementException_WhenGivenArrayOfStringsContainingNegatives()
        {
            var input = new string[] { "1", "b", "-4" };

            Assert.Throws<ArgumentException>(() => _inputParserService.Parse(input));
        }
    }
}
