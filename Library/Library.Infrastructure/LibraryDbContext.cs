using Library.Domain.Data;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Infrastructure;

/// <summary>
/// Контекст базы данных библиотеки с настройкой сущностей и их связей
/// </summary>
public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Типы изданий
    /// </summary>
    public DbSet<EditionType> EditionTypes { get; set; }

    /// <summary>
    /// Издательства
    /// </summary>
    public DbSet<Publisher> Publishers { get; set; }

    /// <summary>
    /// Читатели библиотеки
    /// </summary>
    public DbSet<Reader> Readers { get; set; }

    /// <summary>
    /// Книги
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Выдачи книг
    /// </summary>
    public DbSet<Borrow> Borrows { get; set; }

    /// <summary>
    /// Конфигурирует модели сущностей, их свойства, связи и начальные данные
    /// </summary>
    /// <param name="modelBuilder">Модель билдера</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var stringListConverter = new ValueConverter<List<string>, string>(
            v => string.Join(';', v),
            v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
        );

        var stringListComparer = new ValueComparer<List<string>>(
            (l1, l2) => l1!.SequenceEqual(l2!),
            l => l.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            l => l.ToList()
        );

        var editionTypes = LibraryData.SeedEditionTypes();
        var publishers = LibraryData.SeedPublishers();
        var readers = LibraryData.SeedReaders();
        var books = LibraryData.SeedBooks(editionTypes, publishers);
        var borrows = LibraryData.SeedBorrows(books, readers);

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

            builder.HasData(editionTypes);
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

            builder.HasData(publishers);
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

            builder.HasData(readers);
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

            builder.HasData(books);
        });

        modelBuilder.Entity<Borrow>().ToTable("borrow");
        modelBuilder.Entity<Borrow>(builder =>
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BorrowDate)
                .IsRequired();

            builder.Property(b => b.Days)
                .IsRequired();

            builder.Property(b => b.ReturnDate);

            builder.HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Reader)
                .WithMany()
                .HasForeignKey(b => b.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(borrows);
        });
    }
}