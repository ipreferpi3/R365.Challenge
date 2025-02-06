using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class DivisionServiceTests
    {
        private IDivisionService _divisionService;

        [SetUp]
        public void Setup()
        {
            _divisionService = new DivisionService();
        }

        [Test]
        public void TryDivide_ShouldReturnCorrectTotal_WhenGivenListOfInts()
        {
            var input = new List<int> { 6, 2, 1 };
            var result = _divisionService.TryDivide(input);

            Assert.That(result.Total, Is.EqualTo(3));
        }

        [Test]
        public void TryDivide_ShouldReturnCorrectFormula_WhenGivenListOfInts()
        {
            var input = new List<int> { 6, 2, 1 };
            var result = _divisionService.TryDivide(input);

            Assert.That(result.Formula, Is.EqualTo(string.Format("{0} = {1}", string.Join('/', input), result.Total)));
        }
    }
}
