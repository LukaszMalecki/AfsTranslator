using Afs.Translator.Controllers;
using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Afs.Translator.Models;

namespace Afs.Translator.Tests.Unit
{
    public class ModelValidationTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData(ModelValidationTestsConstants.text0char, false)]
        [InlineData(ModelValidationTestsConstants.text1char, true)]
        [InlineData(ModelValidationTestsConstants.text100char, true)]
        [InlineData(ModelValidationTestsConstants.text101char, false)]
        public void TranslationRequestCreateDto_VariousLengthTextToTranslate_ProperValidation(string toTranslate, bool validationResult)
        {
            //Arrange
            string textToTranslate = toTranslate;
            string translation = TranslatorControllerTestsConstants.DefaultTranslation;
            DateTime? requestDate = null;
            int? translationId = null;
            var sut = new TranslationRequestCreateDto()
            {
                TextToTranslate = textToTranslate,
                TranslationName = translation,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(sut, context, results, true);
            //Assert
            Assert.Equal(validationResult, isValid);
        }
        [Theory]
        [InlineData("\n\r", false)]
        [InlineData("HelloGuys", true)]
        [InlineData("Hello guys", true)]
        [InlineData("Hello guys!", true)]
        [InlineData("Hello guys! : D", true)]
        [InlineData(@"#helloworld:>", true)]
        [InlineData("Hello 123", true)]
        [InlineData("\twelcome\t", false)]
        public void TranslationRequestCreateDto_VariousSpecialCharacters_ProperValidation(string toTranslate, bool validationResult)
        {
            //Arrange
            string textToTranslate = toTranslate;
            string translation = TranslatorControllerTestsConstants.DefaultTranslation;
            DateTime? requestDate = null;
            int? translationId = null;
            var sut = new TranslationRequestCreateDto()
            {
                TextToTranslate = textToTranslate,
                TranslationName = translation,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(sut, context, results, true);
            //Assert
            Assert.Equal(validationResult, isValid);
        }
        [Theory]
        [InlineData(null, false)]
        [InlineData(ModelValidationTestsConstants.text0char, false)]
        [InlineData(ModelValidationTestsConstants.text1char, true)]
        [InlineData(ModelValidationTestsConstants.text50char, true)]
        [InlineData(ModelValidationTestsConstants.text51char, false)]
        public void Translation_VariousLengthTranslationName_ProperValidation(string translationName, bool validationResult)
        {
            //Arrange
            int id = 2;
            string translation = translationName;
            var sut = new Translation()
            {
                Id = id,
                TranslationName = translation
            };
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(sut, context, results, true);
            //Assert
            Assert.Equal(validationResult, isValid);
        }
        [Theory]
        [InlineData(null, false)]
        [InlineData(ModelValidationTestsConstants.text0char, false)]
        [InlineData(ModelValidationTestsConstants.text1char, true)]
        [InlineData(ModelValidationTestsConstants.text100char, true)]
        [InlineData(ModelValidationTestsConstants.text101char, false)]
        public void TranslationRequest_VariousLengthTranslationName_ProperValidation(string toTranslate, bool validationResult)
        {
            //Arrange
            int id = 1;
            string textToTranslate = toTranslate;
            DateTime requestDate = new DateTime(2024, 5, 17);
            int translationId = 1;
            var sut = new TranslationRequest()
            {
                Id = id,
                TextToTranslate = textToTranslate,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(sut, context, results, true);
            //Assert
            Assert.Equal(validationResult, isValid);
        }
        [Theory]
        [InlineData(null, false)]
        [InlineData(ModelValidationTestsConstants.text0char, false)]
        [InlineData(ModelValidationTestsConstants.text1char, true)]
        [InlineData(ModelValidationTestsConstants.text200char, true)]
        [InlineData(ModelValidationTestsConstants.text201char, false)]
        public void TranslationResponse_VariousLengthTranslationName_ProperValidation(string translatedText, bool validationResult)
        {
            //Arrange
            int id = 1;
            string translated = translatedText;
            int translationRequestId = 1;
            var sut = new TranslationResponse()
            {
                Id = id,
                TranslatedText = translated,
                TranslationRequestId = translationRequestId
            };
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(sut, context, results, true);
            //Assert
            Assert.Equal(validationResult, isValid);
        }
    }
}
