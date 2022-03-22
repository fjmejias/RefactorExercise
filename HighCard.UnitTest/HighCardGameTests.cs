using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using Moq;
using NUnit.Framework;
using System;

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
        public void Given_PlayerNames_When_AddPlayers_Then_Players_Created()
        {
            // given
            var firstPlayerName = "player 1";
            var secondPlayerName = "player 2";

            // when
            _sut.AddPlayers(firstPlayerName, secondPlayerName);

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(firstPlayerName, _sut.FirstPlayer.Name);
            Assert.AreEqual(secondPlayerName, _sut.SecondPlayer.Name);
        }

        [Test]
        public void Given_Not_Existing_Players_When_Play_Then_Raised_Exception()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            // when / then
            Assert.Throws<Exception>(() => _sut.Play());
        }

        [Test]
        public void Given_Existing_Players_When_Play_Then_Players_Have_Card()
        {
            // given
            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.FirstPlayer.PlayingCard);
            Assert.IsNotNull(_sut.SecondPlayer.PlayingCard);
        }

        [Test]
        public void Given_PlayerA_Greater_Than_PlayerB_When_Play_Then_PlayerB_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(10, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.IsTrue(_sut.FirstPlayer.Winner);
            Assert.IsFalse(_sut.SecondPlayer.Winner);
        }

        [Test]
        public void Given_PlayerA_Less_Than_PlayerB_When_Play_Then_PlayerA_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(10, Suits.Hearts));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.IsFalse(_sut.FirstPlayer.Winner);
            Assert.IsTrue(_sut.SecondPlayer.Winner);
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

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.Tie, _sut.GameResult);
            Assert.IsFalse(_sut.FirstPlayer.Winner);
            Assert.IsFalse(_sut.SecondPlayer.Winner);
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

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.Play();

            // then
            Assert.IsNotNull(_sut);
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.AreEqual(isPlayerAWinner, _sut.FirstPlayer.Winner);
            Assert.AreEqual(!isPlayerAWinner, _sut.SecondPlayer.Winner);
        }

        #region private methods

        private Card GetCard(int number, Suits suit)
        {
            return new Card { Number = number, Suit = suit };
        }

        #endregion
    }
}
