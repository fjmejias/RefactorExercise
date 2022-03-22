﻿using HighCard.Configuration;
using HighCard.Enums;
using HighCard.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace HighCard.UnitTest
{
    public class CardSelectorTests
    {
        private ICardSelector _sut;
        private Mock<IHighCardSettings> _highCardSettings;

        private const int NumCardsPerDeck = 40;
        private const int NumDecks = 2;

        [SetUp]
        public void SetupTests()
        {
            _highCardSettings = new Mock<IHighCardSettings>();
            _sut = new CardSelector(_highCardSettings.Object);
        }

        [Test]
        public void Given_NumCardsPerDeck_Not_Divisible_By_SuitsNumber_When_InitializeCards_Then_Raised_Exception()
        {
            // given
            _highCardSettings.SetupGet(s => s.NumCardsPerDeck).Returns(1);

            // when / then
            var ex = Assert.Throws<Exception>(() => _sut.InitializeCards());
            TestContext.Progress.WriteLine(ex.Message);
        }

        [Test]
        public void Given_NumCardsPerDeck_Divisible_By_SuitsNumber_When_InitializeCards_Then_Cards_Initialized()
        {
            // given
            _highCardSettings.SetupGet(s => s.NumCardsPerDeck).Returns(NumCardsPerDeck);
            _highCardSettings.SetupGet(s => s.NumDecks).Returns(NumDecks);

            // when
            var result = _sut.InitializeCards();

            // then
            Assert.AreEqual(NumCardsPerDeck * NumDecks, result);
        }

        [Test]
        public void Given_No_CardsInitialized_When_DrawCard_Then_Raised_Exception()
        {
            // when / then
            Assert.Throws<Exception>(() => _sut.DrawCard());
        }

        [Test]
        public void Given_CardsInitialized_When_DrawCard_Then_Returned_Card()
        {
            // given
            _highCardSettings.SetupGet(s => s.NumCardsPerDeck).Returns(NumCardsPerDeck);
            _highCardSettings.SetupGet(s => s.NumDecks).Returns(NumDecks);
            _sut.InitializeCards();

            // when
            var result = _sut.DrawCard();

            // then
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Number > 0);
            Assert.IsTrue(Enum.IsDefined(typeof(Suits), result.Suit));
        }
    }
}