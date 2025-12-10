using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Publisher
/// Реализует основные операции CRUD для сущности <see cref="Publisher"/>
/// </summary>
public class PublisherRepository(LibraryDbContext context) : IRepository<Publisher>
{
    /// <summary>
    /// Получает все издательства из базы данных
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов <see cref="Publisher"/></returns>
    public async Task<IReadOnlyList<Publisher>> GetAll(CancellationToken ct = default) =>
        await context.Publishers
            .AsNoTracking()
            .ToListAsync(ct);

    /// <summary>
    /// Получает издательство по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <param name="includes">Связанные сущности для включения (Eager Loading)</param>
    /// <returns>Объект <see cref="Publisher"/> или <see langword="null"/>, если не найден</returns>
    public async Task<Publisher?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<Publisher, object>>[] includes
    )
    {
        IQueryable<Publisher> query = context.Publishers.AsNoTracking();

        if (includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /// <summary>
    /// Создает новое издательство и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Объект издательства для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект <see cref="Publisher"/></returns>
    public async Task<Publisher> Create(Publisher entity, CancellationToken ct = default)
    {
        var result = await context.Publishers.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующего издательства
    /// </summary>
    /// <param name="entity">Объект <see cref="Publisher"/> с обновленными данными (должен содержать корректный Id)</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект <see cref="Publisher"/> или <see langword="null"/>, если сущность не найдена</returns>
    public async Task<Publisher?> Update(Publisher entity, CancellationToken ct = default)
    {
        var exists = await context.Publishers.AnyAsync(e => e.Id == entity.Id, ct);

        if (!exists)
        {
            return null;
        }

        context.Publishers.Attach(entity).State = EntityState.Modified;

        await context.SaveChangesAsync(ct);

        return entity;
    }

    /// <summary>
    /// Удаляет издательство по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства для удаления</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Publishers
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
        {
            return false;
        }

        context.Publishers.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}