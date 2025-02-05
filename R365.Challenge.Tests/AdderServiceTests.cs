using FakeItEasy;
using R365.Challenge.Interfaces;
using R365.Challenge.Services;

namespace R365.Challenge.Tests
{
    public class AdderServiceTests
    {
        private IAdderService _adderService;

        [SetUp]
        public void Setup()
        {
            _adderService = new AdderService();
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGivenListOfInts()
        {
            var input = new List<int> { 1, 0, 3, 0, 2, 0, 1, 0, 3, 0 };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(10));
        }
    }
}
