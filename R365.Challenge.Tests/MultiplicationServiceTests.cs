using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class MultiplicationServiceTests
    {
        private IMultiplicationService _multiplicationService;

        [SetUp]
        public void Setup()
        {
            _multiplicationService = new MultiplicationService();
        }

        [Test]
        public void TryMultiply_ShouldReturnCorrectTotal_WhenGivenListOfInts()
        {
            var input = new List<int> { 1, 2, 3 };
            var result = _multiplicationService.TryMultiply(input);

            Assert.That(result.Total, Is.EqualTo(6));
        }

        [Test]
        public void TryMultiply_ShouldReturnCorrectFormula_WhenGivenListOfInts()
        {
            var input = new List<int> { 1, 2, 3 };
            var result = _multiplicationService.TryMultiply(input);

            Assert.That(result.Formula, Is.EqualTo(string.Format("{0} = {1}", string.Join('*', input), result.Total)));
        }
    }
}
