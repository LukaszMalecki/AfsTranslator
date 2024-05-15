using Afs.Translator.FunTranslations;
using Xunit;
namespace Afs.Translator.Tests.Unit
{
    public class TranslationClientTests
    {
        private readonly TranslationClient _sut;
        private static readonly string _text500Char = new string('a', 500);
        private static readonly string _text501Char = new string('a', 501);
        private static  readonly string _text1Char = "a";
        public TranslationClientTests()
        {
            _sut = new TranslationClient();
        }
        [Fact]
        public async Task TranslateAsync_NullTextToTranslate_ArgumentNullException()
        {
            //Arrange

            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(null!, "placeholder"));
            //Assert
            var ex = Assert.IsType<ArgumentNullException>(e);
            Assert.Equal("textToTranslate", ex.ParamName);
            Assert.StartsWith("Null", ex.Message);
        }
        [Fact]
        public async Task TranslateAsync_EmptyTextToTranslate_ArgumentOutOfRangeException()
        {
            //Arrange

            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync("", "placeholder"));
            //Assert
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("textToTranslate", ex.ParamName);
            Assert.StartsWith("Empty", ex.Message);
        }
        [Theory]
        [InlineData(TranslationClientTestsConstants.text500Char, true)]
        [InlineData(TranslationClientTestsConstants.text501Char, false)]
        [InlineData(TranslationClientTestsConstants.text1Char, true)]
        public async Task Method_Condition_Expectation(string textToTranslate, bool isValid)
        {
            // Arrange


            // Act
            /*var e = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _sut.TranslateAsync(textToTranslate, "placeholder"));*/
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(textToTranslate, "placeholder"));
            // Assert
            if(isValid)
            {
                Assert.Null(e);
            }
            else
            {
                var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
                Assert.Equal("textToTranslate", ex.ParamName);
                Assert.StartsWith("Length", ex.Message);
            }
        }
    }
}