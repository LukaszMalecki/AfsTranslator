using Afs.Translator.Models;
using Microsoft.EntityFrameworkCore;

namespace Afs.Translator.Data
{
    public static class ModelBuilderExtensions
    {
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
