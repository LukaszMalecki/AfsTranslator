using System.ComponentModel.DataAnnotations;

namespace Afs.Translator.Models
{
    public class TranslationRequest
    {
        public int Id { get; set; }
        [Required]
        [Range(ModelConstants.ModelTranslationRequestTextLenMin, ModelConstants.ModelTranslationRequestTextLenMax, ErrorMessage = "Length of Text to translate must be between {1} and {2}")]
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
