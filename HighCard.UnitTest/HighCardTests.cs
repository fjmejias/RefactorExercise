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
    }
}
