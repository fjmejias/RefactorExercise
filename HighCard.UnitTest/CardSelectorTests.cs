using HighCard.Configuration;
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
        public void Given_Zero_NumCardsPerSuit_By_SuitsNumber_When_InitializeCards_Then_Raised_Exception()
        {
            // given
            _highCardSettings.SetupGet(s => s.NumCardsPerSuit).Returns(0);

            // when / then
            var ex = Assert.Throws<Exception>(() => _sut.InitializeCards());
            TestContext.Progress.WriteLine(ex.Message);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Given_NonZero_NumCardsPerDeck_When_InitializeCards_Then_Cards_Initialized(bool isJokerEnabled)
        {
            // given
            _highCardSettings.SetupGet(s => s.NumCardsPerSuit).Returns(NumCardsPerDeck);
            _highCardSettings.SetupGet(s => s.NumDecks).Returns(NumDecks);
            _highCardSettings.SetupGet(s => s.EnableJoker).Returns(isJokerEnabled);
            var expectedNumOfCards = NumCardsPerDeck * CardSelector.SuitsNumber * NumDecks;

            if (isJokerEnabled)
            {
                expectedNumOfCards += NumDecks;
            }

            // when
            var result = _sut.InitializeCards();

            // then
            Assert.AreEqual(expectedNumOfCards, result);
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
            _highCardSettings.SetupGet(s => s.NumCardsPerSuit).Returns(NumCardsPerDeck);
            _highCardSettings.SetupGet(s => s.NumDecks).Returns(NumDecks);
            _sut.InitializeCards();

            // when
            var result = _sut.DrawCard();

            // then
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Number > 0);
            Assert.IsNotNull(result.Suit);
            Assert.IsTrue(Enum.IsDefined(typeof(Suits), result.Suit));
        }
    }
}
