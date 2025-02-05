using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class CalculatorServiceTests
    {
        private IAdderService _fakeAdderService;
        private IInputParserService _fakeInputParserService;
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _fakeAdderService = A.Fake<IAdderService>();
            _fakeInputParserService = A.Fake<IInputParserService>();
            _calculatorService = new CalculatorService(_fakeAdderService, _fakeInputParserService);
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingNewLineDelimiter()
        {
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(12);

            var result = _calculatorService.Calculate(new string("1\\n2"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 1, 2 })).MustHaveHappened();
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingCommaDelimiter()
        {
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(12);

            var result = _calculatorService.Calculate(new string("1,2"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 1, 2 })).MustHaveHappened();
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingCommaAndNewLineDelimiter()
        {
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2, 3 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(12);

            var result = _calculatorService.Calculate(new string("1\\n2,3"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2", "3" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 1, 2, 3 })).MustHaveHappened();
            Assert.That(result, Is.EqualTo(12));
        }
    }
}