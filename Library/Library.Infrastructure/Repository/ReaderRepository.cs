using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class ReaderRepository(LibraryDbContext context) : IRepository<Reader>
{
    public async Task<Reader> Create(Reader entity, CancellationToken ct = default)
    {
        var result = await context.Readers.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<Reader?> Get(Guid id, CancellationToken ct = default) =>
        await context.Readers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<Reader>> GetAll(CancellationToken ct = default) =>
        await context.Readers
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<Reader?> Update(Reader entity, CancellationToken ct = default)
    {
        var existing = await context.Readers
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Readers
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        context.Readers.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}
