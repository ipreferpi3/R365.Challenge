using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class SubtractionServiceTests
    {
        private ISubtractionService _subtractionService;

        [SetUp]
        public void Setup()
        {
            _subtractionService = new SubtractionService();
        }

        [Test]
        public void TrySubtract_ShouldReturnCorrectTotal_WhenGivenListOfInts()
        {
            var input = new List<int> { 3, 2, 1};
            var result = _subtractionService.TrySubtract(input);

            Assert.That(result.Total, Is.EqualTo(0));
        }

        [Test]
        public void TryAdSubtract_ShouldReturnCorrectFormula_WhenGivenListOfInts()
        {
            var input = new List<int> { 3, 2, 1 };
            var result = _subtractionService.TrySubtract(input);

            Assert.That(result.Formula, Is.EqualTo(string.Format("{0} = {1}", string.Join('-', input), result.Total)));
        }
    }
}
