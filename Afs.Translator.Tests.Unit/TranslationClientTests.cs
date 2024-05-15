using Afs.Translator.FunTranslations;
namespace Afs.Translator.Tests.Unit
{
    public class TranslationClientTests
    {
        [Fact]
        public async Task TranslateAsync_NullTextToTranslate_ArgumentNullException()
        {
            //Arrange
            var sut = new TranslationClient();
            //Act
            var e = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                sut.TranslateAsync(null!, "placeholder"));
            //Assert
            var ex = Assert.IsType<ArgumentNullException>(e);
            Assert.Equal("textToTranslate", ex.ParamName);
            Assert.StartsWith("Null", ex.Message);
        }
        [Fact]
        public async Task TranslateAsync_EmptyTextToTranslate_ArgumentOutOfRangeException()
        {
            //Arrange
            var sut = new TranslationClient();
            //Act
            var e = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                sut.TranslateAsync("", "placeholder"));
            //Assert
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("textToTranslate", ex.ParamName);
            Assert.StartsWith("Empty", ex.Message);
        }
    }
}