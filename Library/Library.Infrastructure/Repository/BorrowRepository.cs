using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class BorrowRepository(LibraryDbContext context) : IRepository<Borrow>
{
    public async Task<Borrow> Create(Borrow entity, CancellationToken ct = default)
    {
        var result = await context.Borrows.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<Borrow?> Get(Guid id, CancellationToken ct = default) =>
        await context.Borrows
            .AsNoTracking()
            .Include(b => b.Book)
            .Include(b => b.Reader)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<Borrow>> GetAll(CancellationToken ct = default) =>
        await context.Borrows
            .AsNoTracking()
            .Include(b => b.Book)
            .Include(b => b.Reader)
            .ToListAsync(ct);

    public async Task<Borrow?> Update(Borrow entity, CancellationToken ct = default)
    {
        var existing = await context.Borrows
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync(ct);

        await context.Entry(existing).Reference(b => b.Book).LoadAsync(ct);
        await context.Entry(existing).Reference(b => b.Reader).LoadAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Borrows
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        context.Borrows.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}