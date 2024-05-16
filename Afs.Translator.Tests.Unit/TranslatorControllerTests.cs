using Afs.Translator.Controllers;
using Afs.Translator.FunTranslations;
using System.Transactions;
using Xunit;

namespace Afs.Translator.Tests.Unit
{
    public class TranslatorControllerTests
    {
        private readonly TranslatorController _sut;
        public TranslatorControllerTests()
        {
            _sut = new TranslatorController(
                new TranslationClient(
                    new HttpClientStub(
                        () => TranslatorControllerTestsConstants.DefaultSuccessResponseMessage)
                ));
        }
        [Fact]
        public async Task GetTranslatedAsync_NullTextToTranslate_ArgumentNullException()
        {
            //Arrange
            string textToTranslate = null!;
            var translation = TranslatorControllerTestsConstants.DefaultTranslation;
            var expectedTranslated = TranslatorControllerTestsConstants.DefaultTranslated;
            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.GetTranslatedAsync(textToTranslate, translation));
            //Assert
            var ex = Assert.IsType<ArgumentNullException>(e);
            Assert.Equal("textToTranslate", ex.ParamName);
            Assert.StartsWith("Null", ex.Message);
        }
        [Fact]
        public async Task GetTranslatedAsync_DefaultParametersSuccess_MatchingExpectedTranslated()
        {
            //Arrange
            var textToTranslate = TranslatorControllerTestsConstants.DefaultText;
            var translation = TranslatorControllerTestsConstants.DefaultTranslation;
            var expectedTranslated = TranslatorControllerTestsConstants.DefaultTranslated;
            //Act
            var translationResponse = await _sut.GetTranslatedAsync(textToTranslate, translation);
            //Assert
            Assert.NotNull(translationResponse);
            Assert.Equal(expectedTranslated, translationResponse.TranslatedText);
        }
    }
}
