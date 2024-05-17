using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Afs.Translator.Models
{
    [Index(nameof(TranslationName), IsUnique=true)]
    public class Translation
    {
        public int Id { get; set; }
        [Required]
        [Range(ModelConstants.ModelTranslationNameLenMin, ModelConstants.ModelTranslationNameLenMax, ErrorMessage = "Length of translation name must be between {1} and {2}")]
        public string TranslationName { get; set; }
    }
}
