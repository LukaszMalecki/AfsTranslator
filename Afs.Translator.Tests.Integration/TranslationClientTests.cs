using Afs.Translator.FunTranslations;

namespace Afs.Translator.Tests.Integration
{
    public class TranslationClientTests
    {
        private TranslationClient _sut;
        public TranslationClientTests() 
        {
            _sut = new TranslationClient(
                new HttpClientWrapper()
                );
        }
        [Fact]
        public async Task TranslateAsync_DefaultInput_IsSuccess()
        {
            //Arrange
            var textToTranslate = TranslationClientTestsConstants.DefaultTextToTranslate;
            var translation = TranslationClientTestsConstants.DefaultTranslation;
            var expectedResponse = TranslationClientTestsConstants.DefaultDeserializedResponse;
            //Act
            var translationResponse = await _sut.TranslateAsync(textToTranslate, translation);
            //Assert
            Assert.NotNull(translationResponse);
            Assert.Equal(expectedResponse.Success, translationResponse.Success);
        }
        [Fact]
        public async Task TranslateAsync_MisspelledTranslation_ArgumentOutOfRangeException()
        {
            //Arrange
            var textToTranslate = TranslationClientTestsConstants.DefaultTextToTranslate;
            var translation = "yeetspeak";
            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.TranslateAsync(textToTranslate, translation));
            // Assert
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("translation", ex.ParamName);
            Assert.StartsWith("Translation", ex.Message);
        }
    }
}