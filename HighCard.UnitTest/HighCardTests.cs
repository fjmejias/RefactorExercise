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
        public void Given_PlayerA_Less_Than_PlayerB_When_Play_Then_Win()
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next()).Returns(1).Returns(10);
            
            // when
            var result = _sut.Play();

            // then
            Assert.AreEqual(HighCardResult.Win, result);
        }

        [Test]
        public void Given_PlayerA_Greater_Than_PlayerB_When_Play_Then_Lose()
        {
            // given
            _randomGeneratorMock.SetupSequence(r => r.Next()).Returns(10).Returns(1);
            
            // when
            var result = _sut.Play();

            // then
            Assert.AreEqual(HighCardResult.Lose, result);
        }
    }
}
