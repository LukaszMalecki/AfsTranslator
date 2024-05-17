using System.ComponentModel.DataAnnotations;

namespace Afs.Translator.Models
{
    public class TranslationRequest
    {
        public int Id { get; set; }
        [Required]
        //[Range(ModelConstants.ModelTranslationRequestTextLenMin, ModelConstants.ModelTranslationRequestTextLenMax, ErrorMessage = "Length of Text to translate must be between {1} and {2}")]
        [MinLength(ModelConstants.ModelTranslationRequestTextLenMin, ErrorMessage = "Minimum required length is {1}")]
        [MaxLength(ModelConstants.ModelTranslationRequestTextLenMax, ErrorMessage = "Maximum allowed length is {1}")]
        [RegularExpression(@"^[ -}]+$")]
        public string TextToTranslate { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public int TranslationId { get; set; }
        public Translation Translation { get; set; }
        public TranslationResponse? Response { get; set; }
    }
}
