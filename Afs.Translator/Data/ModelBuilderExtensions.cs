using Afs.Translator.Models;
using Microsoft.EntityFrameworkCore;

namespace Afs.Translator.Data
{
    public static class ModelBuilderExtensions
    {
        //MaxLengthAnnotation with constant didn't set nvarchar length in sql code
        public static void SeedLenMax(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Translation>()
                .Property(p => p.TranslationName)
                .HasMaxLength(ModelConstants.ModelTranslationNameLenMax);
            modelBuilder.Entity<TranslationRequest>()
                .Property(p => p.TextToTranslate)
                .HasMaxLength(ModelConstants.ModelTranslationRequestTextLenMax);
            modelBuilder.Entity<TranslationResponse>()
               .Property(p => p.TranslatedText)
               .HasMaxLength(ModelConstants.ModelTranslationResponseTextLenMax);
        }
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Translation>().HasData
            (
                new Translation()
                {
                    Id = ModelConstants.DefaultTranslationId,
                    TranslationName = ModelConstants.DefaultTranslation
                }
                
            );
        }
    }
}
