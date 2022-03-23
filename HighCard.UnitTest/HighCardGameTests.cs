using HighCard.Contracts.Enums;
using HighCard.Contracts.Interfaces;
using HighCard.Contracts.Models;
using Moq;
using NUnit.Framework;
using System;

namespace HighCard.UnitTest
{
    public class HighCardGameTests
    {
        private ICardGame _sut;
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
        public void Given_Not_Existing_Players_When_PlayCards_Then_Raised_Exception()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            // when / then
            Assert.Throws<Exception>(() => _sut.PlayCards());
            Assert.IsNull(_sut.FirstPlayer);
            Assert.IsNull(_sut.SecondPlayer);
        }

        [Test]
        public void Given_Existing_Players_When_PlayCards_Then_Players_Have_Card()
        {
            // given
            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            // when
            _sut.PlayCards();

            // then
            Assert.IsNotNull(_sut.FirstPlayer.PlayingCard);
            Assert.IsNotNull(_sut.SecondPlayer.PlayingCard);
        }

        [Test]
        public void Given_FirstPlayer_Greater_Than_SecondPlayer_When_PlayCards_Then_SecondPlayer_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(10, Suits.Clubs)).Returns(GetCard(1, Suits.Hearts));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.PlayCards();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.IsTrue(_sut.FirstPlayer.Winner);
            Assert.IsFalse(_sut.SecondPlayer.Winner);
        }

        [Test]
        public void Given_FirstPlayer_Less_Than_SecondPlayer_When_PlayCards_Then_FirstPlayer_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, Suits.Clubs)).Returns(GetCard(10, Suits.Hearts));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.PlayCards();

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
        public void Given_FirstPlayer_Value_And_Suit_Equal_To_SecondPlayer_AValue_And_Suit_When_PlayCards_Then_Tie(Suits suitA, Suits suitB)
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, suitA)).Returns(GetCard(1, suitB));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.PlayCards();

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
        public void Given_FirstPlayer_Value_Equal_To_SecondPlayer_Value_But_Different_Suit_When_PlayCards_Then_Expected_Winner(Suits suitA, Suits suitB, bool isFirstPlayerWinner)
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetCard(1, suitA)).Returns(GetCard(1, suitB));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.PlayCards();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.AreEqual(isFirstPlayerWinner, _sut.FirstPlayer.Winner);
            Assert.AreEqual(!isFirstPlayerWinner, _sut.SecondPlayer.Winner);
        }

        [Test]
        public void Given_One_Player_Has_Joker_When_PlayCards_When_FirstPlayer_Wins()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetJoker()).Returns(GetCard(1, Suits.Clubs));

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.PlayCards();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.PlayerWins, _sut.GameResult);
            Assert.IsTrue(_sut.FirstPlayer.Winner);
            Assert.IsFalse(_sut.SecondPlayer.Winner);
        }

        [Test]
        public void Given_Both_Players_Have_Joker_When_PlayCards_When_Tied_Game()
        {
            // given
            _cardSelectorMock.SetupSequence(r => r.DrawCard())
                .Returns(GetJoker()).Returns(GetJoker());

            _sut.AddPlayers("firstPlayerName", "secondPlayerName");

            // when
            _sut.PlayCards();

            // then
            Assert.IsNotNull(_sut.FirstPlayer);
            Assert.IsNotNull(_sut.SecondPlayer);
            Assert.AreEqual(GameResult.Tie, _sut.GameResult);
            Assert.IsTrue(_sut.FirstPlayer.Winner);
            Assert.IsTrue(_sut.SecondPlayer.Winner);
        }

        #region private methods

        private Card GetCard(int number, Suits suit)
        {
            return new Card { Number = number, Suit = suit };
        }

        private Card GetJoker()
        {
            return new Card { IsJoker = true };
        }

        #endregion
    }
}
