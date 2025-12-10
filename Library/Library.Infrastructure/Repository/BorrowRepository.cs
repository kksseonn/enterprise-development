using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Borrow
/// Реализует основные операции CRUD для сущности <see cref="Borrow"/>
/// </summary>
public class BorrowRepository(LibraryDbContext context) : IRepository<Borrow>
{
    /// <summary>
    /// Получает все записи о выдачах книг
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов <see cref="Borrow"/></returns>
    public async Task<IReadOnlyList<Borrow>> GetAll(CancellationToken ct = default) =>
        await context.Borrows
            .AsNoTracking()
            .Include(b => b.Book)
            .Include(b => b.Reader)
            .ToListAsync(ct);

    /// <summary>
    /// Получает запись о выдаче книги по ее уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <param name="ct">Токен отмены</param>
    /// <param name="includes">Связанные сущности для дополнительного включения (Eager Loading)</param>
    /// <returns>Объект <see cref="Borrow"/> или <see langword="null"/>, если не найден</returns>
    public async Task<Borrow?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<Borrow, object>>[] includes
    )
    {
        IQueryable<Borrow> query = context.Borrows;

        query = query.Include(b => b.Book!).Include(b => b.Reader!);

        if (includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /// <summary>
    /// Создает новую запись о выдаче книги и сохраняет ее в базе данных
    /// </summary>
    /// <param name="entity">Объект выдачи для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект <see cref="Borrow"/></returns>
    public async Task<Borrow> Create(Borrow entity, CancellationToken ct = default)
    {
        var result = await context.Borrows.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующей записи о выдаче книги
    /// </summary>
    /// <param name="entity">Объект <see cref="Borrow"/> с обновленными данными (должен содержать корректный Id)</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект <see cref="Borrow"/> или <see langword="null"/>, если сущность не найдена</returns>
    public async Task<Borrow?> Update(Borrow entity, CancellationToken ct = default)
    {
        try
        {
            await context.SaveChangesAsync(ct);
            return entity;
        }
        catch (DbUpdateConcurrencyException)
        {
            return null;
        }
    }

    /// <summary>
    /// Удаляет запись о выдаче книги по ее уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи для удаления</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Borrows
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
        {
            return false;
        }

        context.Borrows.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}