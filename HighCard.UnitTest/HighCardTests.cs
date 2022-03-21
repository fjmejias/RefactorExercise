using System;
using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using Moq;
using NUnit.Framework;

namespace HighCard.UnitTest
{
    public class HighCardTests
    {
        private IHighCard _sut;
        private Mock<IRandomGenerator> _randomGeneratorMock;

        [SetUp]
        public void SetupTests()
        {
            _randomGeneratorMock = new Mock<IRandomGenerator>();

            _sut = new HighCard(_randomGeneratorMock.Object);
        }

        [Test]
        public void Given_PlayerA_Greater_Than_PlayerB_When_Play_Then_PlayerB_Wins()
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(10).Returns(1);
            _randomGeneratorMock.SetupSequence(r => r.Next(Card.SuitsNumber)).Returns((int)Suits.Diamonds).Returns((int)Suits.Spades);

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, result.GameResult);
            Assert.IsTrue(result.PlayerA.Winner);
            Assert.IsFalse(result.PlayerB.Winner);
        }

        [Test]
        public void Given_PlayerA_Less_Than_PlayerB_When_Play_Then_PlayerA_Wins()
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(1).Returns(10);
            _randomGeneratorMock.SetupSequence(r => r.Next(Card.SuitsNumber)).Returns((int)Suits.Hearts).Returns((int)Suits.Clubs);

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, result.GameResult);
            Assert.IsFalse(result.PlayerA.Winner);
            Assert.IsTrue(result.PlayerB.Winner);
        }

        [TestCase(Suits.Clubs, Suits.Clubs)]
        [TestCase(Suits.Diamonds, Suits.Diamonds)]
        [TestCase(Suits.Hearts, Suits.Hearts)]
        [TestCase(Suits.Spades, Suits.Spades)]
        public void Given_PlayerA_Value_And_Suit_Equal_To_PlayerB_AValue_And_Suit_When_Play_Then_Tie(Suits suitA, Suits suitB)
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(4).Returns(4);
            _randomGeneratorMock.SetupSequence(r => r.Next(Card.SuitsNumber)).Returns((int)suitA).Returns((int)suitB);

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.Tie, result.GameResult);
            Assert.IsFalse(result.PlayerA.Winner);
            Assert.IsFalse(result.PlayerB.Winner);
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
            _randomGeneratorMock.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(4).Returns(4);
            _randomGeneratorMock.SetupSequence(r => r.Next(Card.SuitsNumber)).Returns((int)suitA).Returns((int)suitB);

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, result.GameResult);
            Assert.AreEqual(isPlayerAWinner, result.PlayerA.Winner);
            Assert.AreEqual(!isPlayerAWinner, result.PlayerB.Winner);
        }
    }
}
