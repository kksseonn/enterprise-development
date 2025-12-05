using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class PublisherRepository(LibraryDbContext context) : IRepository<Publisher>
{
    public async Task<Publisher> Create(Publisher entity, CancellationToken ct = default)
    {
        var result = await context.Publishers.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<Publisher?> Get(Guid id, CancellationToken ct = default) =>
    await context.Publishers
        .AsNoTracking()
        .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<Publisher>> GetAll(CancellationToken ct = default) =>
        await context.Publishers
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<Publisher?> Update(Publisher entity, CancellationToken ct = default)
    {
        var existing = await context.Publishers.FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Publishers.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;

        context.Publishers.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}