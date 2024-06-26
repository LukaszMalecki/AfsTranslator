﻿using Afs.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Afs.Translator.DTOs
{
    public class TranslationRequestCreateDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "The text field is required")]
        [FromQuery(Name = "text")]
        /*[BindRequired, Range(ModelConstants.InputTextLenMin, 
            ModelConstants.InputTextLenMax, ErrorMessage = $"Length of the given text must be between 1 and 100") ]*/
        [MinLength(ModelConstants.InputTextLenMin, ErrorMessage = "Minimum required length is {1}")]
        [MaxLength(ModelConstants.InputTextLenMax, ErrorMessage = "Maximum allowed length is {1}")]
        //!@#^*()_=+[]{}:;',.<>/?-
        [RegularExpression(ModelConstants.TextToTranslateRegex, ErrorMessage = ModelConstants.TextoToTranslateError)]
        public string TextToTranslate { get; set; }
        [FromQuery(Name = "date")]
        public DateTime? RequestDate { get; set; }
        [FromQuery(Name = "translation_id")]
        public int? TranslationId { get; set; }
        [FromQuery(Name = "translation")]
        [RegularExpression(@"^[a-z _-]+$", ErrorMessage = ModelConstants.TranslationNameError)]
        public string? TranslationName { get; set; }
    }
}
