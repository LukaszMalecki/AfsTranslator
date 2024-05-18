using Afs.Translator.Data;
using Afs.Translator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Afs.Translator.Tests.Sintegration
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TranslatorDbTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        context.AddRange(
                        new Translation { TranslationName = "leetspeak" });
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public TranslatorDbContextTest CreateContext()
            => new TranslatorDbContextTest(
                new DbContextOptionsBuilder<TranslatorDbContextTest>()
                    .UseSqlServer(ConnectionString)
                    .Options);
        //without seed
        public class TranslatorDbContextTest : Data.TranslatorDbContext
        {
            public TranslatorDbContextTest(DbContextOptions options) : base(options)
            {
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.SeedLenMax();
            }
        }
    }
}

