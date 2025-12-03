using Library.Domain.Entities;
using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;
public class LibraryDbContext(DbContextOptions<LibraryDbContext> options, LibraryData data) : DbContext(options)
{
    public DbSet<EditionType> EditionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EditionType>(builder =>
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasData(data.EditionTypes);
        });
    }
}


