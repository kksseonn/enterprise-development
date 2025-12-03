using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class EditionTypeRepository(LibraryDbContext context) : IRepository<EditionType>
{
    public async Task<EditionType> Create(EditionType entity, CancellationToken ct = default)
    {
        var result = await context.EditionTypes.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<EditionType?> Get(Guid id, CancellationToken ct = default) =>
    await context.EditionTypes
        .AsNoTracking()
        .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<EditionType>> GetAll(CancellationToken ct = default) =>
        await context.EditionTypes
            .AsNoTracking()
            .ToListAsync(ct);


    public async Task<EditionType?> Update(EditionType entity, CancellationToken ct = default)
    {
        // Проверяем наличие
        var existing = await context.EditionTypes.FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        // Обновляем только изменённые поля
        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.EditionTypes.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;

        context.EditionTypes.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}
