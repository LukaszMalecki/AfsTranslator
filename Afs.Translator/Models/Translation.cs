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
        [MinLength(ModelConstants.ModelTranslationNameLenMin, ErrorMessage = "Minimum required length is {1}")]
        [MaxLength(ModelConstants.ModelTranslationNameLenMax, ErrorMessage = "Maximum allowed length is {1}")]
        public string TranslationName { get; set; }
    }
}
