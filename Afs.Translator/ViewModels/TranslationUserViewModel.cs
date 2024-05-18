using Afs.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Afs.Translator.ViewModels
{
    public class TranslationUserViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "The text field is required")]
        /*[BindRequired, Range(ModelConstants.InputTextLenMin, 
            ModelConstants.InputTextLenMax, ErrorMessage = $"Length of the given text must be between 1 and 100") ]*/
        [MinLength(ModelConstants.InputTextLenMin, ErrorMessage = "Minimum required length is {1}")]
        [MaxLength(ModelConstants.InputTextLenMax, ErrorMessage = "Maximum allowed length is {1}")]
        //!@#^*()_=+[]{}:;',.<>/?-
        [RegularExpression(ModelConstants.TextToTranslateRegex, ErrorMessage = ModelConstants.TextoToTranslateError)]
        [Display(Name = "Text to translate")]
        public string TextToTranslate { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? TranslationId { get; set; }
        [MinLength(1)]
        [Display(Name = "Translated text")]
        public string? TranslatedText { get; set; }
    }
}
