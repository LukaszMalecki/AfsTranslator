using Afs.Translator.FunTranslations;
using Xunit;
namespace Afs.Translator.Tests.Unit
{
    public class TranslationClientTests
    {
        private readonly TranslationClient _sut;
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
        [Theory]
        [InlineData(TranslationClientTestsConstants.text500Char, true)]
        [InlineData(TranslationClientTestsConstants.text501Char, false)]
        [InlineData(TranslationClientTestsConstants.text1Char, true)]
        [InlineData(TranslationClientTestsConstants.text0Char, false)]
        public async Task TranslateAsync_VariousLengthTexts_ArgumentOutOfRangeExceptionIfInvalid(string textToTranslate, bool isValid)
        {
            // Arrange


            // Act
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
        [Theory]
        [InlineData("\t", false)]
        [InlineData("\n", false)]
        [InlineData("\r", false)]
        [InlineData(TranslationClientTestsConstants.text500Char+"\t\t\r", true)]
        [InlineData("\n"+TranslationClientTestsConstants.text500Char, true)]
        [InlineData("\ra", true)]
        public async Task TranslateAsync_TextsWithLeadingWhitespaceCharacters_LengthCheckedAfterTrimming(string textToTranslate, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(textToTranslate, "placeholder"));
            // Assert
            if (isValid)
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
        [Theory]
        [InlineData("aa\raaa", false)]
        [InlineData("hello\nhi", false)]
        [InlineData("welco\tme", true)]
        [InlineData("\r\na\n", true)]
        public async Task TranslateAsync_TextsWithNewlineCharactersBetweenOtherChars_ArgumentExceptionIfNotValid(string textToTranslate, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(textToTranslate, "placeholder"));
            // Assert
            if (isValid)
            {
                Assert.Null(e);
            }
            else
            {
                var ex = Assert.IsType<ArgumentException>(e);
                Assert.Equal("textToTranslate", ex.ParamName);
                Assert.StartsWith("Newline", ex.Message);
            }
        }


    }
}