using Library.Domain.Data;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options)
    : DbContext(options)
{
    public DbSet<EditionType> EditionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EditionType>().ToTable("edition_type");

        var editionType = LibraryData.EditionTypes();
        modelBuilder.Entity<EditionType>().HasData(editionType);
    }
}
