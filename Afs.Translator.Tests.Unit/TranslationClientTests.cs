using Afs.Translator.FunTranslations;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Xunit;
namespace Afs.Translator.Tests.Unit
{
    public class TranslationClientTests
    {
        private readonly TranslationClient _sut;
        public TranslationClientTests()
        {
            _sut = new TranslationClient(
                new HttpClientStub(
                    TranslationClientTestsConstants.DefaultResponseFunc, 
                    TranslationClientTestsConstants.DefaultSuccessResponseMessageFunc)
                );
        }
        [Fact]
        public async Task TranslateAsync_NullTextToTranslate_ArgumentNullException()
        {
            //Arrange

            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(null!, TranslationClientTestsConstants.ValidTranslationPlaceholder));
            //Assert
            var ex = Assert.IsType<ArgumentNullException>(e);
            Assert.Equal("textToTranslate", ex.ParamName);
            Assert.StartsWith("Null", ex.Message);
        }
        [Theory]
        [InlineData(TranslationClientTestsConstants.Text500Char, true)]
        [InlineData(TranslationClientTestsConstants.Text501Char, false)]
        [InlineData(TranslationClientTestsConstants.Text1Char, true)]
        [InlineData(TranslationClientTestsConstants.Text0Char, false)]
        public async Task TranslateAsync_VariousLengthTexts_ArgumentOutOfRangeExceptionIfInvalid(string textToTranslate, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(textToTranslate, TranslationClientTestsConstants.ValidTranslationPlaceholder));
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
        [InlineData(TranslationClientTestsConstants.Text500Char+"\t\t\r", true)]
        [InlineData("\n"+TranslationClientTestsConstants.Text500Char, true)]
        [InlineData("\ra", true)]
        public async Task TranslateAsync_TextsWithLeadingWhitespaceCharacters_LengthCheckedAfterTrimming(string textToTranslate, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(textToTranslate, TranslationClientTestsConstants.ValidTranslationPlaceholder));
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
                _sut.TranslateAsync(textToTranslate, TranslationClientTestsConstants.ValidTranslationPlaceholder));
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
        [Fact]
        public async Task TranslateAsync_NullTranslation_ArgumentNullException()
        {
            //Arrange

            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(TranslationClientTestsConstants.ValidTextToTranslate, null!));
            //Assert
            var ex = Assert.IsType<ArgumentNullException>(e);
            Assert.Equal("translation", ex.ParamName);
            Assert.StartsWith("Null", ex.Message);
        }
        [Theory]
        [InlineData(TranslationClientTestsConstants.Text50Char, true)]
        [InlineData(TranslationClientTestsConstants.Text51Char, false)]
        [InlineData(TranslationClientTestsConstants.Text1Char, true)]
        [InlineData(TranslationClientTestsConstants.Text0Char, false)]
        public async Task TranslateAsync_VariousLengthTranslations_ArgumentOutOfRangeExceptionIfInvalid(string translation, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(TranslationClientTestsConstants.ValidTextToTranslate, translation));
            // Assert
            if (isValid)
            {
                Assert.Null(e);
            }
            else
            {
                var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
                Assert.Equal("translation", ex.ParamName);
                Assert.StartsWith("Length", ex.Message);
            }
        }
        [Theory]
        [InlineData("\t", false)]
        [InlineData("\n", false)]
        [InlineData("\r", false)]
        [InlineData(TranslationClientTestsConstants.Text50Char + "\t\t\r", true)]
        [InlineData("\n" + TranslationClientTestsConstants.Text50Char, true)]
        [InlineData("\ra", true)]
        public async Task TranslateAsync_TranslationsWithLeadingWhitespaceCharacters_LengthCheckedAfterTrimming(string translation, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(TranslationClientTestsConstants.ValidTextToTranslate, translation));
            // Assert
            if (isValid)
            {
                Assert.Null(e);
            }
            else
            {
                var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
                Assert.Equal("translation", ex.ParamName);
                Assert.StartsWith("Length", ex.Message);
            }
        }
        [Theory]
        [InlineData("aa\raaa", false)]
        [InlineData("hello\nhi", false)]
        [InlineData("welco\tme", true)]
        [InlineData("\r\na\n", true)]
        public async Task TranslateAsync_TranslationsWithNewlineCharactersBetweenOtherChars_ArgumentExceptionIfNotValid(string translation, bool isValid)
        {
            // Arrange


            // Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(TranslationClientTestsConstants.ValidTextToTranslate, translation));
            // Assert
            if (isValid)
            {
                Assert.Null(e);
            }
            else
            {
                var ex = Assert.IsType<ArgumentException>(e);
                Assert.Equal("translation", ex.ParamName);
                Assert.StartsWith("Newline", ex.Message);
            }
        }
        [Fact]
        public async Task TranslateAsync_DefaultInput_EqualsExpectedDefault()
        {
            //Arrange
            var sut = new TranslationClient(
                new HttpClientStub(
                    TranslationClientTestsConstants.DefaultResponseFunc,
                    TranslationClientTestsConstants.DefaultSuccessResponseMessageFunc
                    )
                );
            var textToTranslate = TranslationClientTestsConstants.DefaultTextToTranslate;
            var translation = TranslationClientTestsConstants.DefaultTranslation;
            var expectedResponse = TranslationClientTestsConstants.DefaultDeserializedResponse;
            //Act
            var translationResponse = await sut.TranslateAsync(textToTranslate, translation);
            //Assert
            Assert.Equal(expectedResponse, translationResponse);
        }
        [Fact]
        public async Task TranslateAsync_ApiReturnsEmpty_InvalidOperationException()
        {
            //Arrange
            var sut = new TranslationClient(new HttpClientStub(() => null!, () => null!));
            var textToTranslate = TranslationClientTestsConstants.DefaultTextToTranslate;
            var translation = TranslationClientTestsConstants.DefaultTranslation;
            //Act
            var e = await Record.ExceptionAsync(() =>
                sut.TranslateAsync(TranslationClientTestsConstants.ValidTextToTranslate, translation));
            //Assert
            var ex = Assert.IsType<InvalidOperationException>(e);
            Assert.StartsWith("Null", ex.Message);
        }
        [Fact]
        public async Task TranslateAsync_MisspelledTranslation_ArgumentOutOfRangeException()
        {
            //Arrange
            var sut = new TranslationClient(
                new HttpClientStub(
                    TranslationClientTestsConstants.DefaultResponseFunc,
                    TranslationClientTestsConstants.DefaultNoTranslationResponseMessageFunc
                    )
                );
            //var sut = new TranslationClient(new HttpClientStub(TranslationClientTestsConstants.DefaultResponse));
            var textToTranslate = TranslationClientTestsConstants.DefaultTextToTranslate;
            var translation = "yeetspeak";
            var expectedResponse = TranslationClientTestsConstants.DefaultDeserializedResponse;
            //Act
            var e = await Record.ExceptionAsync(() =>
                sut.TranslateAsync(textToTranslate, translation));
            // Assert
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("translation", ex.ParamName);
            Assert.StartsWith("Translation", ex.Message);
        }


    }
}