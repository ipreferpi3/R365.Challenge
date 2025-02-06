using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Models;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class CalculatorServiceTests
    {
        private IAdditionService _fakeAdditionService;
        private ISubtractionService _fakeSubtractionService;
        private IMultiplicationService _fakeMultiplicationService;
        private IDivisionService _fakeDivisionService;
        private IInputParserService _fakeInputParserService;
        private IDelimiterParserService _fakeDelimiterParserService;
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _fakeAdditionService = A.Fake<IAdditionService>();
            _fakeSubtractionService = A.Fake<ISubtractionService>();
            _fakeMultiplicationService = A.Fake<IMultiplicationService>();
            _fakeDivisionService = A.Fake<IDivisionService>();
            _fakeInputParserService = A.Fake<IInputParserService>();
            _fakeDelimiterParserService = A.Fake<IDelimiterParserService>();

            _calculatorService = new CalculatorService(_fakeAdditionService, _fakeSubtractionService,
                _fakeMultiplicationService, _fakeDivisionService,
                _fakeInputParserService, _fakeDelimiterParserService);
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "1\\n2",
                CalculationType = CalculationTypes.Addition
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2 });
            A.CallTo(() => _fakeAdditionService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2" })).MustHaveHappened();
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "1,2",
                CalculationType = CalculationTypes.Addition
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2 });
            A.CallTo(() => _fakeAdditionService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2" })).MustHaveHappened();
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "1\n2,3",
                CalculationType = CalculationTypes.Addition
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 1, 2, 3 });
            A.CallTo(() => _fakeAdditionService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "1", "2", "3" })).MustHaveHappened();
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "//#\n2#5",
                CalculationType = CalculationTypes.Addition
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 2, 5 });
            A.CallTo(() => _fakeAdditionService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeInputParserService.Parse(new string[] { "2", "5" })).MustHaveHappened();
            Assert.That(result.Total, Is.EqualTo(12));
            Assert.That(result.Formula, Is.EqualTo("1+2=3"));
        }

        [Test]
        public void Calculate_ShouldCallAdditionService_WhenAdditionChosenAsInput()
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "//#\n2#5",
                CalculationType = CalculationTypes.Addition
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 2, 5 });
            A.CallTo(() => _fakeAdditionService.TryAdd(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeAdditionService.TryAdd(A<List<int>>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Calculate_ShouldCallSubtractionService_WhenSubtractionChosenAsInput()
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "//#\n2#5",
                CalculationType = CalculationTypes.Subtraction
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 2, 5 });
            A.CallTo(() => _fakeSubtractionService.TrySubtract(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeSubtractionService.TrySubtract(A<List<int>>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Calculate_ShouldCallMultiplicationService_WhenMultiplicationChosenAsInput()
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "//#\n2#5",
                CalculationType = CalculationTypes.Multiplication
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 2, 5 });
            A.CallTo(() => _fakeMultiplicationService.TryMultiply(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeMultiplicationService.TryMultiply(A<List<int>>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Calculate_ShouldCallDivisionService_WhenDivisionChosenAsInput()
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

            var calculationRequest = new CalculationRequest
            {
                CalculationString = "//#\n2#5",
                CalculationType = CalculationTypes.Division
            };

            A.CallTo(() => _fakeDelimiterParserService.GetDelimiters(A<string>.Ignored)).Returns(calculation);
            A.CallTo(() => _fakeInputParserService.Parse(A<string[]>.Ignored)).Returns(new List<int> { 2, 5 });
            A.CallTo(() => _fakeDivisionService.TryDivide(A<List<int>>.Ignored)).Returns(calculationResult);

            var result = _calculatorService.Calculate(calculationRequest);

            A.CallTo(() => _fakeDivisionService.TryDivide(A<List<int>>.Ignored)).MustHaveHappened();
        }
    }
}