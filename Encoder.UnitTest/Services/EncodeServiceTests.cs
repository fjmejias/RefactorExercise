using Encoder.Interfaces;
using Encoder.Services;
using NUnit.Framework;

namespace Encoder.UnitTest.Services
{
    public class EncodeServiceTests
    {
        private IEncodeService _sut;

        [SetUp]
        public void SetupTests()
        {
            _sut = new EncodeService();
        }

        [Test]
        public void Given_InputText_When_Encode_Then_Expected_Encoded_Text()
        {
            // given
            var inputText = "This is an input test string";

            // when
            var result = _sut.Encode(inputText);

            // then
            Assert.AreEqual("VGhpcyBpcyBhbiBpbnB1dCB0ZXN0IHN0cmluZw==", result);
        }

        [Test]
        public void Given_EncodedText_When_Decode_Then_Expected_Decoded_Text()
        {
            // given
            var encodedText = "VGhpcyBpcyBhbiBpbnB1dCB0ZXN0IHN0cmluZw==";

            // when
            var result = _sut.Decode(encodedText);

            // then
            Assert.AreEqual("This is an input test string", result);
        }

        [Test]
        public void Given_InputText_When_Encode_And_Decode_Then_Expected_InputText()
        {
            // given
            var inputText = "This is an input test string";

            // when
            var result = _sut.Decode(_sut.Encode(inputText));

            // then
            Assert.AreEqual(inputText, result);
        }
    }
}
