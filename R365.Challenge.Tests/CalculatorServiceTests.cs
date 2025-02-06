using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Models;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class CalculatorServiceTests
    {
        private IAdderService _fakeAdderService;
        private IInputParserService _fakeInputParserService;
        private IDelimiterParserService _fakeDelimiterParserService;
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _fakeAdderService = A.Fake<IAdderService>();
            _fakeInputParserService = A.Fake<IInputParserService>();
            _fakeDelimiterParserService = A.Fake<IDelimiterParserService>();
            _calculatorService = new CalculatorService(_fakeAdderService, _fakeInputParserService, _fakeDelimiterParserService);
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingNewLineDelimiter()
        {
            var calculation = new Calculation
            {
                Delimiters = new List<string> { ",", "\\n" },
            };

            var calculationResult = new CalculationResult
            {
                Total = 12,
                Formula = "1+2=3"
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(new string("1\\n2"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 1, 2 })).MustHaveHappened();
            Assert.That(result.Total, Is.EqualTo(12));
            Assert.That(result.Formula, Is.EqualTo("1+2=3"));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingCommaDelimiter()
        {
            var calculation = new Calculation
            {
                Delimiters = new List<string> { ",", "\n" },
            };

            var calculationResult = new CalculationResult
            {
                Total = 12,
                Formula = "1+2=3"
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(new string("1,2"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 1, 2 })).MustHaveHappened();
            Assert.That(result.Total, Is.EqualTo(12));
            Assert.That(result.Formula, Is.EqualTo("1+2=3"));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingCommaAndNewLineDelimiter()
        {
            var calculation = new Calculation
            {
                Delimiters = new List<string> { ",", "\n" },
            };

            var calculationResult = new CalculationResult
            {
                Total = 12,
                Formula = "1+2=3"
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2, 3 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(new string("1\n2,3"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2", "3" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 1, 2, 3 })).MustHaveHappened();
            Assert.That(result.Total, Is.EqualTo(12));
            Assert.That(result.Formula, Is.EqualTo("1+2=3"));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGivenValidInputUsingCustomDelimiter()
        {
            var calculation = new Calculation
            {
                Delimiters = new List<string> { ",", "\\n", "#" },
                OperandStartIndex = 4
            };

            var calculationResult = new CalculationResult
            {
                Total = 12,
                Formula = "1+2=3"
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 2, 5 });
            A.CallTo(() => _fakeAdderService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(new string("//#\n2#5"));

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "2", "5" })).MustHaveHappened();
            A.CallTo(() => _fakeAdderService.TryAdd(new List<int> { 2, 5 })).MustHaveHappened();
            Assert.That(result.Total, Is.EqualTo(12));
            Assert.That(result.Formula, Is.EqualTo("1+2=3"));
        }
    }
}