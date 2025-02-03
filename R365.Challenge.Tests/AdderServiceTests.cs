﻿using FakeItEasy;
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
        public void TryAdd_ShouldReturnCorrectSum_WhenGiven5IntsAndNonIntsAsInput()
        {
            var input = new string[] { "1", "d", "3", "", "2", "adf", "1", "qwerty", "3", "@^(" };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGiven3IntsAsInput()
        {
            var input = new string[] { "1", "2", "3" };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGiven2IntsAsInput()
        {
            var input = new string[] { "1", "2" };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGiven1IntsAsInput()
        {
            var input = new string[] { "1" };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGivenEmptyInput()
        {
            var input = new string[] { };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGivenIntAndNonIntAsInput()
        {
            var input = new string[] { "1", "x" };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void TryAdd_ShouldReturnCorrectSum_WhenGiven2NonIntAsInput()
        {
            var input = new string[] { "y", "x" };
            var result = _adderService.TryAdd(input);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
