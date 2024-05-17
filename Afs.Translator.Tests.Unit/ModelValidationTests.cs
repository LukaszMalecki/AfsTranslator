using Afs.Translator.Controllers;
using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using Xunit;

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
        public void TranslationRequestCreateDto_VariousTextToTranslate_ProperValidation(string toTranslate, bool validationResult)
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
    }
}
