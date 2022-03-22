using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using Moq;
using NUnit.Framework;

namespace HighCard.UnitTest
{
    public class HighCardGameTests
    {
        private IHighCardGame _sut;
        private Mock<ICardSelector> _cardSelectorMock;

        [SetUp]
        public void SetupTests()
        {
            _cardSelectorMock = new Mock<ICardSelector>();
            _sut = new HighCardGame(_cardSelectorMock.Object);
        }

        [Test]
        public void Given_PlayerA_Greater_Than_PlayerB_When_Play_Then_PlayerB_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(10, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.PlayerA);
            Assert.IsNotNull(_sut.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.IsTrue(_sut.PlayerA.Winner);
            Assert.IsFalse(_sut.PlayerB.Winner);
        }

        [Test]
        public void Given_PlayerA_Less_Than_PlayerB_When_Play_Then_PlayerA_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(10, Suits.Hearts));

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.PlayerA);
            Assert.IsNotNull(_sut.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.IsFalse(_sut.PlayerA.Winner);
            Assert.IsTrue(_sut.PlayerB.Winner);
        }

        [TestCase(Suits.Clubs, Suits.Clubs)]
        [TestCase(Suits.Diamonds, Suits.Diamonds)]
        [TestCase(Suits.Hearts, Suits.Hearts)]
        [TestCase(Suits.Spades, Suits.Spades)]
        public void Given_PlayerA_Value_And_Suit_Equal_To_PlayerB_AValue_And_Suit_When_Play_Then_Tie(Suits suitA, Suits suitB)
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, suitA)).Returns(GetCard(1, suitB));

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.PlayerA);
            Assert.IsNotNull(_sut.PlayerB);
            Assert.AreEqual(GameResult.Tie, _sut.GameResult);
            Assert.IsFalse(_sut.PlayerA.Winner);
            Assert.IsFalse(_sut.PlayerB.Winner);
        }

        [TestCase(Suits.Hearts, Suits.Diamonds, true)]
        [TestCase(Suits.Hearts, Suits.Clubs, true)]
        [TestCase(Suits.Hearts, Suits.Spades, true)]
        [TestCase(Suits.Diamonds, Suits.Hearts, false)]
        [TestCase(Suits.Diamonds, Suits.Clubs, true)]
        [TestCase(Suits.Diamonds, Suits.Spades, true)]
        [TestCase(Suits.Clubs, Suits.Hearts, false)]
        [TestCase(Suits.Clubs, Suits.Diamonds, false)]
        [TestCase(Suits.Clubs, Suits.Spades, true)]
        [TestCase(Suits.Spades, Suits.Hearts, false)]
        [TestCase(Suits.Spades, Suits.Diamonds, false)]
        [TestCase(Suits.Spades, Suits.Clubs, false)]
        public void Given_PlayerA_Value_Equal_To_PlayerB_Value_But_Different_Suit_When_Play_Then_Expected_Winner(Suits suitA, Suits suitB, bool isPlayerAWinner)
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, suitA)).Returns(GetCard(1, suitB));

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut);
            Assert.IsNotNull(_sut.PlayerA);
            Assert.IsNotNull(_sut.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.AreEqual(isPlayerAWinner, _sut.PlayerA.Winner);
            Assert.AreEqual(!isPlayerAWinner, _sut.PlayerB.Winner);
        }

        #region private methods

        private Card GetCard(int number, Suits suit)
        {
            return new Card { Number = number, Suit = suit };
        }

        #endregion
    }
}
