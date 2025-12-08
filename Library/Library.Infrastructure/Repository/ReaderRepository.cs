using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Reader
/// </summary>
public class ReaderRepository(LibraryDbContext context) : IRepository<Reader>
{
    private readonly LibraryDbContext _context = context;

    /// <summary>
    /// Создает нового читателя и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Объект читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект Reader</returns>
    public async Task<Reader> Create(Reader entity, CancellationToken ct = default)
    {
        var result = await _context.Readers.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Получает читателя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Объект Reader или null, если не найден</returns>
    public async Task<Reader?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<Reader, object>>[] includes
    )
    {
        IQueryable<Reader> query = _context.Readers.AsNoTracking();

        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /// <summary>
    /// Получает всех читателей
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех читателей</returns>
    public async Task<IReadOnlyList<Reader>> GetAll(CancellationToken ct = default) =>
        await _context.Readers
            .AsNoTracking()
            .ToListAsync(ct);

    /// <summary>
    /// Обновляет данные существующего читателя
    /// </summary>
    /// <param name="entity">Объект Reader с обновленными данными</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект Reader или null, если не найден</returns>
    public async Task<Reader?> Update(Reader entity, CancellationToken ct = default)
    {
        var existing = await _context.Readers
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync(ct);

        return existing;
    }

    /// <summary>
    /// Удаляет читателя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Readers
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        _context.Readers.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}