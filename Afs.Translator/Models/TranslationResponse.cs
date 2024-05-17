using System.ComponentModel.DataAnnotations;

namespace Afs.Translator.Models
{
    public class TranslationResponse
    {
        public int Id { get; set; }
        [Required]
        [Range(ModelConstants.ModelTranslationResponseTextLenMin, ModelConstants.ModelTranslationResponseTextLenMax, ErrorMessage = "Length of Translated text must be between {1} and {2}")]
        public string TranslatedText { get; set; }
        [Required]
        public int TranslationRequestId { get; set; }
        public TranslationRequest TranslationRequest { get; set; }
    }
}
