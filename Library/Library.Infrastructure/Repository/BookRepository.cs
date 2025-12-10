using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Book
/// Реализует основные операции CRUD для сущности <see cref="Book"/>
/// </summary>
public class BookRepository(LibraryDbContext context) : IRepository<Book>
{
    /// <summary>
    /// Получает все книги из базы данных
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов <see cref="Book"/></returns>
    public async Task<IReadOnlyList<Book>> GetAll(CancellationToken ct = default) =>
        await context.Books
            .AsNoTracking()
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .ToListAsync(ct);

    /// <summary>
    /// Получает книгу по ее уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <param name="includes">Связанные сущности для дополнительного включения (Eager Loading)</param>
    /// <returns>Объект <see cref="Book"/> или <see langword="null"/>, если не найден</returns>
    public async Task<Book?> Get(
        Guid id,
        CancellationToken ct = default,
        params Expression<Func<Book, object>>[] includes
    )
    {
        IQueryable<Book> query = context.Books;

        query = query.Include(b => b.EditionType!).Include(b => b.Publisher!);

        if (includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }


    /// <summary>
    /// Создает новую книгу и сохраняет ее в базе данных
    /// </summary>
    /// <param name="entity">Объект книги для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект <see cref="Book"/></returns>
    public async Task<Book> Create(Book entity, CancellationToken ct = default)
    {
        var result = await context.Books.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующей книги
    /// </summary>
    /// <param name="entity">Объект <see cref="Book"/> с обновленными данными (должен содержать корректный Id)</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект <see cref="Book"/> или <see langword="null"/>, если сущность не найдена</returns>
    public async Task<Book?> Update(Book entity, CancellationToken ct = default)
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
    /// Удаляет книгу по ее уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги для удаления</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Books
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
        {
            return false;
        }

        context.Books.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }
}