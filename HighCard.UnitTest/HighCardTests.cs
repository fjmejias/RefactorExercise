using HighCard.Enums;
using HighCard.Interfaces;
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

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, result.GameResult);
            Assert.IsTrue(result.PlayerA.Victory);
            Assert.IsFalse(result.PlayerB.Victory);
        }

        [Test]
        public void Given_PlayerA_Less_Than_PlayerB_When_Play_Then_PlayerA_Wins()
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(1).Returns(10);

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.PlayerWins, result.GameResult);
            Assert.IsFalse(result.PlayerA.Victory);
            Assert.IsTrue(result.PlayerB.Victory);
        }

        [Test]
        public void Given_PlayerA_Equal_To_PlayerB_When_Play_Then_Tie()
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(4).Returns(4);

            // when
            var result = _sut.Play();

            // then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PlayerA);
            Assert.IsNotNull(result.PlayerB);
            Assert.AreEqual(GameResult.Tie, result.GameResult);
            Assert.IsFalse(result.PlayerA.Victory);
            Assert.IsFalse(result.PlayerB.Victory);
        }
    }
}
