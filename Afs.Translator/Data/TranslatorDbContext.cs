using Afs.Translator.Models;
using Microsoft.EntityFrameworkCore;
using Afs.Translator.ViewModels;

namespace Afs.Translator.Data
{
    public class TranslatorDbContext : DbContext
    {
        public TranslatorDbContext(DbContextOptions options) : base(options) { }
        public DbSet<TranslationRequest> TranslationRequests { get; set; }
        public DbSet<TranslationResponse> TranslationResponses { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedLenMax();
            modelBuilder.Seed();
        }

        public DbSet<Afs.Translator.ViewModels.TranslationUserViewModel>? TranslationUserViewModel { get; set; }
    }
}
