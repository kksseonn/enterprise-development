using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Reader
/// Реализует основные операции CRUD для сущности <see cref="Reader"/>
/// </summary>
public class ReaderRepository(LibraryDbContext context) : IRepository<Reader>
{
    /// <summary>
    /// Получает всех читателей из базы данных
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов <see cref="Reader"/></returns>
    public async Task<IReadOnlyList<Reader>> GetAll(CancellationToken ct = default) =>
        await context.Readers
            .AsNoTracking()
            .ToListAsync(ct);

    /// <summary>
    /// Получает читателя по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <param name="includes">Связанные сущности для включения (Eager Loading)</param>
    /// <returns>Объект <see cref="Reader"/> или <see langword="null"/>, если не найден</returns>
    public async Task<Reader?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<Reader, object>>[] includes
    )
    {
        IQueryable<Reader> query = context.Readers.AsNoTracking();

        if (includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /// <summary>
    /// Создает нового читателя и сохраняет его в базе данных
    /// </summary>
    /// <param name="entity">Объект читателя для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект <see cref="Reader"/></returns>
    public async Task<Reader> Create(Reader entity, CancellationToken ct = default)
    {
        var result = await context.Readers.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующего читателя
    /// </summary>
    /// <param name="entity">Объект <see cref="Reader"/> с обновленными данными (должен содержать корректный Id)</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект <see cref="Reader"/> или <see langword="null"/>, если сущность не найдена</returns>
    public async Task<Reader?> Update(Reader entity, CancellationToken ct = default)
    {
        var existing = await context.Readers
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
        {
            return null;
        }

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync(ct);

        return existing;
    }

    /// <summary>
    /// Удаляет читателя по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор читателя для удаления</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Readers
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
        {
            return false;
        }

        context.Readers.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}