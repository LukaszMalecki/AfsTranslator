using System.ComponentModel.DataAnnotations;

namespace Afs.Translator.DTOs
{
    public record TranslationResponseBriefDto
    {
        [Required]
        [MinLength(1)]
        public string TranslatedText { get; set; }
    }
}
