using Microsoft.EntityFrameworkCore;
using UrlShortererApp.Models;
using UrlShortererApp.Services;

namespace UrlShorterer.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UrlModel> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UrlModel>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlService.numsOfChars);
                builder.HasIndex(s => s.Code).IsUnique();
            }
            );
        }
    }
}