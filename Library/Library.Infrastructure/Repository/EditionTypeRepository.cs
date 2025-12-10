using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью EditionType
/// Реализует основные операции CRUD для сущности <see cref="EditionType"/>
/// </summary>
public class EditionTypeRepository(LibraryDbContext context) : IRepository<EditionType>
{
    /// <summary>
    /// Получает все типы изданий из базы данных
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов <see cref="EditionType"/></returns>
    public async Task<IReadOnlyList<EditionType>> GetAll(CancellationToken ct = default) =>
        await context.EditionTypes
            .AsNoTracking()
            .ToListAsync(ct);

    /// <summary>
    /// Получает тип издания по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <param name="includes">Связанные сущности для включения (Eager Loading)</param>
    /// <returns>Объект <see cref="EditionType"/> или <see langword="null"/>, если не найден</returns>
    public async Task<EditionType?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<EditionType, object>>[] includes
    )
    {
        IQueryable<EditionType> query = context.EditionTypes.AsNoTracking();

        if (includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /// <summary>
    /// Создает новый тип издания и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Объект типа издания для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект <see cref="EditionType"/></returns>
    public async Task<EditionType> Create(EditionType entity, CancellationToken ct = default)
    {
        var result = await context.EditionTypes.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующего типа издания
    /// </summary>
    /// <param name="entity">Объект <see cref="EditionType"/> с обновленными данными (должен содержать корректный Id)</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект <see cref="EditionType"/> или <see langword="null"/>, если сущность не найдена</returns>
    public async Task<EditionType?> Update(EditionType entity, CancellationToken ct = default)
    {
        var exists = await context.EditionTypes.AnyAsync(e => e.Id == entity.Id, ct);

        if (!exists)
        {
            return null;
        }

        context.EditionTypes.Attach(entity).State = EntityState.Modified;

        await context.SaveChangesAsync(ct);

        return entity;
    }

    /// <summary>
    /// Удаляет тип издания по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания для удаления</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.EditionTypes
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
        {
            return false;
        }

        context.EditionTypes.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}