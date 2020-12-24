using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Boilerplate.Store.Entities;

namespace Boilerplate.Store
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Used by the EF cli tools
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder
                    .UseNpgsql(configuration.GetConnectionString("StoreContext"));
            }

            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>();

            modelBuilder.Entity<Book>();
        }

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
    }
}
