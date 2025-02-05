using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class CalculatorServiceTests
    {
        private IAdderService _fakeAdderService;
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _fakeAdderService = A.Fake<IAdderService>();
            _calculatorService = new CalculatorService(_fakeAdderService);
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGiveValidInputUsingNewLineDelimiter()
        {
            A.CallTo(() => _fakeAdderService.TryAdd(A<string[]>.Ignored)).Returns(12);

            var result = _calculatorService.Calculate(new string("1,2"));

            A.CallTo(() => _fakeAdderService.TryAdd(new string[] { "1", "2" })).MustHaveHappened();
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGiveValidInputUsingCommaDelimiter()
        {
            A.CallTo(() => _fakeAdderService.TryAdd(A<string[]>.Ignored)).Returns(12);

            var result = _calculatorService.Calculate(new string("1\\n2"));

            A.CallTo(() => _fakeAdderService.TryAdd(new string[] { "1", "2" })).MustHaveHappened();
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void Calculate_ShouldReturnCorrectValue_WhenGiveValidInputUsingCommaAndNewLineDelimiter()
        {
            A.CallTo(() => _fakeAdderService.TryAdd(A<string[]>.Ignored)).Returns(12);

            var result = _calculatorService.Calculate(new string("1\\n2,3"));

            A.CallTo(() => _fakeAdderService.TryAdd(new string[] { "1", "2", "3" })).MustHaveHappened();
            Assert.That(result, Is.EqualTo(12));
        }
    }
}