using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class BookRepository(LibraryDbContext context) : IRepository<Book>
{
    public async Task<Book> Create(Book entity, CancellationToken ct = default)
    {
        var result = await context.Books.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<Book?> Get(Guid id, CancellationToken ct = default) =>
        await context.Books
            .AsNoTracking()
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<Book>> GetAll(CancellationToken ct = default) =>
        await context.Books
            .AsNoTracking()
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .ToListAsync(ct);

    public async Task<Book?> Update(Book entity, CancellationToken ct = default)
    {
        var existing = await context.Books
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        context.Entry(existing).CurrentValues.SetValues(entity);

        existing.Authors = entity.Authors;

        await context.SaveChangesAsync(ct);

        await context.Entry(existing).Reference(b => b.EditionType).LoadAsync(ct);
        await context.Entry(existing).Reference(b => b.Publisher).LoadAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Books
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        context.Books.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}