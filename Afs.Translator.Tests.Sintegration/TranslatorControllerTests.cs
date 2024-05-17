using Afs.Translator.Controllers;
using Afs.Translator.Data;
using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Afs.Translator.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Xunit;

namespace Afs.Translator.Tests.Sintegration
{
    public class TranslatorControllerTests : IClassFixture<TestDatabaseFixture>
    {

        public TestDatabaseFixture Fixture { get; }
        private readonly TranslatorController _sut;
        private readonly TranslatorDbContext _context;
        private readonly INowWrapper _nowWrapper;
        public TranslatorControllerTests(TestDatabaseFixture fixture)
        {
            Fixture = fixture;
            _context = Fixture.CreateContext();
            _nowWrapper = new NowWrapperStub(TranslatorControllerTestsConstants.DefaultDateTime);
            _sut = new TranslatorController(
                new TranslationClient(
                    new HttpClientStub(
                        () => TranslatorControllerTestsConstants.DefaultSuccessResponseMessage)
                ), _context, _nowWrapper);
        }
        protected Tuple<TranslatorDbContext, TranslatorController> ArrangeForTransactionalTests()
        {
            var context = Fixture.CreateContext();
            var sut = new TranslatorController(
                new TranslationClient(
                    new HttpClientStub(
                        () => TranslatorControllerTestsConstants.DefaultSuccessResponseMessage)
                ), _context, _nowWrapper);
            return new Tuple<TranslatorDbContext, TranslatorController>(context, sut);
        }
        [Fact]
        public async Task GetTranslatedAsync_NullTextToTranslate_ArgumentNullException()
        {
            //Arrange
            string textToTranslate = null!;
            var translation = TranslatorControllerTestsConstants.DefaultTranslation;
            var expectedTranslated = TranslatorControllerTestsConstants.DefaultTranslated;
            _context.Database.BeginTransaction();
            //Act
            var e = await Record.ExceptionAsync(() =>
                _sut.GetTranslatedAsync(textToTranslate, translation));
            _context.ChangeTracker.Clear();
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
            _context.Database.BeginTransaction();
            //Act
            var translationResponse = await _sut.GetTranslatedAsync(textToTranslate, translation);
            _context.ChangeTracker.Clear();
            //Assert
            Assert.NotNull(translationResponse);
            Assert.Equal(expectedTranslated, translationResponse.TranslatedText);
        }
        [Fact]
        public async Task TranslateApiAsync_ModelInvalid_BadRequestObjectResult()
        {
            //Arrange
            var textToTranslate = "";
            var translation = TranslatorControllerTestsConstants.DefaultTranslation;
            DateTime? requestDate = null;
            int? translationId = null;
            var expectedTranslated = TranslatorControllerTestsConstants.DefaultTranslated;
            var inputDto = new TranslationRequestCreateDto()
            {
                TextToTranslate = textToTranslate,
                TranslationName = translation,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            _sut.ViewData.ModelState.AddModelError("text", "error");
            _context.Database.BeginTransaction();
            //Act
            var translationResponse = await _sut.TranslateApiAsync(inputDto);
            _context.ChangeTracker.Clear();
            //Assert
            Assert.NotNull(translationResponse);
            Assert.IsType<BadRequestObjectResult>(translationResponse);
        }
        [Theory]
        [InlineData(null, null, true)]
        [InlineData("notthere", null, false)]
        [InlineData("notthere", TranslatorControllerTestsConstants.DefaultTranslationId, false)]
        [InlineData(null, 50000, false)]
        [InlineData(TranslatorControllerTestsConstants.DefaultTranslation, null, true)]
        [InlineData(null, TranslatorControllerTestsConstants.DefaultTranslationId, true)]
        [InlineData(TranslatorControllerTestsConstants.DefaultTranslation, 50000, true)]
        public async Task TranslateApiAsync_SpecifiedTranslationNotFoundInDatabase_BadRequestObjectResultIfInvalid(string? translationName, int? transId, bool isValid)
        {
            //CreateSut(true);
            //Arrange
            var textToTranslate = TranslatorControllerTestsConstants.DefaultText;
            var translation = translationName;
            DateTime? requestDate = null;
            int? translationId = transId;
            var expectedTranslated = TranslatorControllerTestsConstants.DefaultTranslated;
            var inputDto = new TranslationRequestCreateDto()
            {
                TextToTranslate = textToTranslate,
                TranslationName = translation,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            _context.Database.BeginTransaction();
            //Act
            var translationResponse = await _sut.TranslateApiAsync(inputDto);
            _context.ChangeTracker.Clear();
            //Assert
            Assert.NotNull(translationResponse);
            if(!isValid)
                Assert.IsType<BadRequestObjectResult>(translationResponse);
            else
                Assert.IsType<OkObjectResult>(translationResponse);
        }
        [Fact]
        public async Task TranslateApiAsync_ModelValid_ItemAddedToDbContextTranslationRequests()
        {
            //Arrange
            (var context, var sut) = ArrangeForTransactionalTests();
            var textToTranslate = TranslatorControllerTestsConstants.DefaultText;
            var translation = TranslatorControllerTestsConstants.DefaultTranslation;
            DateTime? requestDate = null;
            int? translationId = null;
            var expectedTranslated = TranslatorControllerTestsConstants.DefaultTranslated;
            var inputDto = new TranslationRequestCreateDto()
            {
                TextToTranslate = textToTranslate,
                TranslationName = translation,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            int requestCountBefore = context.TranslationRequests.Count();
            context.Database.BeginTransaction();
            //Act
            var translationResponse = await sut.TranslateApiAsync(inputDto);
            context.ChangeTracker.Clear();
            int requestCountAfter = context.TranslationRequests.Count();
            var addedItem = context.TranslationRequests.FirstOrDefault(x => x.TextToTranslate.Equals(inputDto.TextToTranslate));
            
            //Assert
            Assert.NotNull(addedItem);
            Assert.Equal(requestCountBefore + 1, requestCountAfter);
            Assert.Equal(inputDto.TextToTranslate, addedItem.TextToTranslate);
        }
    }
}
