using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Book
/// </summary>
public class BookRepository(LibraryDbContext context) : IRepository<Book>
{
    private readonly LibraryDbContext _context = context;

    /// <summary>
    /// Создает новую книгу и сохраняет ее в базе данных
    /// </summary>
    /// <param name="entity">Объект Book</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект Book</returns>
    public async Task<Book> Create(Book entity, CancellationToken ct = default)
    {
        var result = await _context.Books.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Получает книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Объект Book или null, если не найден</returns>
    public async Task<Book?> Get(Guid id, CancellationToken ct = default) =>
        await _context.Books
            .AsNoTracking()
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

    /// <summary>
    /// Получает все книги
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов Book</returns>
    public async Task<IReadOnlyList<Book>> GetAll(CancellationToken ct = default) =>
        await _context.Books
            .AsNoTracking()
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .ToListAsync(ct);

    /// <summary>
    /// Обновляет данные существующей книги
    /// </summary>
    /// <param name="entity">Объект Book с обновленными данными</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект Book или null, если не найден</returns>
    public async Task<Book?> Update(Book entity, CancellationToken ct = default)
    {
        var existing = await _context.Books
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);

        // Обновляем навигационное свойство Authors
        existing.Authors = entity.Authors;

        await _context.SaveChangesAsync(ct);

        // Загружаем навигационные свойства
        await _context.Entry(existing).Reference(b => b.EditionType).LoadAsync(ct);
        await _context.Entry(existing).Reference(b => b.Publisher).LoadAsync(ct);

        return existing;
    }

    /// <summary>
    /// Удаляет книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Books
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        _context.Books.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}