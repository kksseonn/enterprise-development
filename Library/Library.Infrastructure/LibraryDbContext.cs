using Library.Domain.Data;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        modelBuilder.Entity<EditionType>().ToTable("edition_type");
        modelBuilder.Entity<EditionType>(builder =>
        {
            builder.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Publisher>().ToTable("publisher");
        modelBuilder.Entity<Publisher>(builder => 
        {
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
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


    }
}
