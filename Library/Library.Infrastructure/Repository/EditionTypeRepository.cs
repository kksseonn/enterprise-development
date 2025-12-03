using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class EditionTypeRepository(LibraryDbContext context) : IRepository<EditionType>
{
    public async Task<EditionType> Create(EditionType entity)
    {
        var result = await context.EditionTypes.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await context.EditionTypes.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;

        context.EditionTypes.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<EditionType?> Get(Guid id) =>
        await context.EditionTypes.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IList<EditionType>> GetAll() =>
        await context.EditionTypes.ToListAsync();

    public async Task<EditionType> Update(EditionType entity)
    {
        context.EditionTypes.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
