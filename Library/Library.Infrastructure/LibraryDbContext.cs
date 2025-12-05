using Library.Domain.Data;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Infrastructure;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options)
    : DbContext(options)
{

    public DbSet<EditionType> EditionTypes { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var stringListConverter = new ValueConverter<List<string>, string>(
                    v => string.Join(';', v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
        );

        var stringListComparer = new ValueComparer<List<string>>(
            (l1, l2) => l1!.SequenceEqual(l2!),
            l => l.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            l => l.ToList()
        );

        modelBuilder.Entity<EditionType>().ToTable("edition_type");
        modelBuilder.Entity<EditionType>(builder =>
        {
            builder.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany<Book>()
                .WithOne(b => b.EditionType)
                .HasForeignKey(b => b.EditionTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Publisher>().ToTable("publisher");
        modelBuilder.Entity<Publisher>(builder =>
        {
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany<Book>()
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Reader>().ToTable("reader");
        modelBuilder.Entity<Reader>(builder =>
        {
            builder.Property(r => r.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Patronymic)
                .HasMaxLength(100);

            builder.Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(r => r.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.RegistrationDate)
                .IsRequired();
        });

        modelBuilder.Entity<Book>().ToTable("book");
        modelBuilder.Entity<Book>(builder =>
        {
            builder.Property(r => r.InventoryNumber)
                .IsRequired();

            builder.Property(r => r.CatalogCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Authors)
                .HasConversion(stringListConverter)
                .IsRequired()
                .HasMaxLength(250)
                .Metadata.SetValueComparer(stringListComparer);

            builder.Property(r => r.EditionTypeId)
                .IsRequired();

            builder.Property(r => r.PublisherId)
                .IsRequired();

            builder.Property(r => r.Year)
                .IsRequired();
        });
    }
}
