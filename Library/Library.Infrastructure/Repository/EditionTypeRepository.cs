using Library.Domain.IRepository;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

public class EditionTypeRepository : IEditionTypeRepository
{
    private readonly LibraryDbContext _context;

    public EditionTypeRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<EditionType> AddAsync(EditionType entity, CancellationToken ct = default)
    {
        var result = await _context.EditionTypes.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.EditionTypes.FirstOrDefaultAsync(e => e.Id == id, ct);
        if (entity == null)
            return false;

        _context.EditionTypes.Remove(entity);
        await _context.SaveChangesAsync(ct);

        return true;
    }

    public async Task<EditionType?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.EditionTypes.FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IEnumerable<EditionType>> GetAllAsync(CancellationToken ct = default) =>
        await _context.EditionTypes.ToListAsync(ct);

    public async Task<EditionType> UpdateAsync(EditionType entity, CancellationToken ct = default)
    {
        _context.EditionTypes.Update(entity);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default) =>
        await _context.EditionTypes.AnyAsync(e => e.Id == id, ct);
}
