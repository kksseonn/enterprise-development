using Library.Domain.Entities;

namespace Library.Domain.IRepository;

public interface IEditionTypeRepository
{
    public Task<IEnumerable<EditionType>> GetAllAsync(CancellationToken ct = default);
    public Task<EditionType?> GetByIdAsync(Guid id, CancellationToken ct = default);
    public Task<EditionType> AddAsync(EditionType entity, CancellationToken ct = default);
    public Task<EditionType> UpdateAsync(EditionType entity, CancellationToken ct = default);
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
