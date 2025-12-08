using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Publisher
/// </summary>
public class PublisherRepository(LibraryDbContext context) : IRepository<Publisher>
{
    private readonly LibraryDbContext _context = context;

    /// <summary>
    /// Создает новое издательство и сохраняет в базе данных
    /// </summary>
    /// <param name="entity">Объект Publisher</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект Publisher</returns>
    public async Task<Publisher> Create(Publisher entity, CancellationToken ct = default)
    {
        var result = await _context.Publishers.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Получает издательство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Объект Publisher или null, если не найден</returns>
    public async Task<Publisher?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<Publisher, object>>[] includes // ✅ Добавлен includes
    )
    {
        IQueryable<Publisher> query = _context.Publishers.AsNoTracking();

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
    /// Получает всех издательств
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех издательств</returns>
    public async Task<IReadOnlyList<Publisher>> GetAll(CancellationToken ct = default) =>
        await _context.Publishers
            .AsNoTracking()
            .ToListAsync(ct);

    /// <summary>
    /// Обновляет данные существующего издательства
    /// </summary>
    /// <param name="entity">Объект Publisher с обновленными данными</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект Publisher или null, если не найден</returns>
    public async Task<Publisher?> Update(Publisher entity, CancellationToken ct = default)
    {
        var exists = await _context.Publishers.AnyAsync(e => e.Id == entity.Id, ct);
        if (!exists)
            return null;

        _context.Publishers.Attach(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync(ct);

        return entity;
    }

    /// <summary>
    /// Удаляет издательство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Publishers
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        _context.Publishers.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}