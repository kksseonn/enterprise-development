using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью EditionType
/// </summary>
public class EditionTypeRepository(LibraryDbContext context) : IRepository<EditionType>
{
    private readonly LibraryDbContext _context = context;

    /// <summary>
    /// Создает новый тип издания и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Объект EditionType</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект EditionType</returns>
    public async Task<EditionType> Create(EditionType entity, CancellationToken ct = default)
    {
        var result = await _context.EditionTypes.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Получает тип издания по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Объект EditionType или null, если не найден</returns>
    public async Task<EditionType?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<EditionType, object>>[] includes
    )
    {
        IQueryable<EditionType> query = _context.EditionTypes.AsNoTracking();

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
    /// Получает все типы изданий
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех EditionType</returns>
    public async Task<IReadOnlyList<EditionType>> GetAll(CancellationToken ct = default) =>
        await _context.EditionTypes
            .AsNoTracking()
            .ToListAsync(ct);

    /// <summary>
    /// Обновляет данные существующего типа издания
    /// </summary>
    /// <param name="entity">Объект EditionType с обновленными данными</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект EditionType или null, если не найден</returns>
    public async Task<EditionType?> Update(EditionType entity, CancellationToken ct = default)
    {
        var exists = await _context.EditionTypes.AnyAsync(e => e.Id == entity.Id, ct);
        if (!exists)
            return null;

        _context.EditionTypes.Attach(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync(ct);

        return entity;
    }

    /// <summary>
    /// Удаляет тип издания по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.EditionTypes
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        _context.EditionTypes.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}