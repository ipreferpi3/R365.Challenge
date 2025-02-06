using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class AdditionServiceTests
    {
        private IAdditionService _additionService;

        [SetUp]
        public void Setup()
        {
            _additionService = new AdditionService();
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGivenListOfInts()
        {
            var input = new List<int> { 1, 0, 3, 0, 2, 0, 1, 0, 3, 0 };
            var result = _additionService.TryAdd(input);

            Assert.That(result.Total, Is.EqualTo(10));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectFormula_WhenGivenListOfInts()
        {
            var input = new List<int> { 1, 0, 3, 0, 2, 0, 1, 0, 3, 0 };
            var result = _additionService.TryAdd(input);

            Assert.That(result.Formula, Is.EqualTo(string.Format("{0} = {1}", string.Join('+', input), result.Total)));
        }
    }
}
