using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с сущностью Borrow
/// </summary>
public class BorrowRepository(LibraryDbContext context) : IRepository<Borrow>
{
    private readonly LibraryDbContext _context = context;

    /// <summary>
    /// Создает новую выдачу книги и сохраняет ее в базе данных
    /// </summary>
    /// <param name="entity">Объект Borrow</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект Borrow</returns>
    public async Task<Borrow> Create(Borrow entity, CancellationToken ct = default)
    {
        var result = await _context.Borrows.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    /// <summary>
    /// Получает выдачу книги по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Объект Borrow или null, если не найден</returns>
    public async Task<Borrow?> Get(Guid id, CancellationToken ct = default) =>
        await _context.Borrows
            .AsNoTracking()
            .Include(b => b.Book)
            .Include(b => b.Reader)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

    /// <summary>
    /// Получает все выдачи книг
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех объектов Borrow</returns>
    public async Task<IReadOnlyList<Borrow>> GetAll(CancellationToken ct = default) =>
        await _context.Borrows
            .AsNoTracking()
            .Include(b => b.Book)
            .Include(b => b.Reader)
            .ToListAsync(ct);

    /// <summary>
    /// Обновляет данные существующей выдачи книги
    /// </summary>
    /// <param name="entity">Объект Borrow с обновленными данными</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект Borrow или null, если не найден</returns>
    public async Task<Borrow?> Update(Borrow entity, CancellationToken ct = default)
    {
        var existing = await _context.Borrows
            .FirstOrDefaultAsync(e => e.Id == entity.Id, ct);

        if (existing == null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync(ct);

        // Загружаем навигационные свойства
        await _context.Entry(existing).Reference(b => b.Book).LoadAsync(ct);
        await _context.Entry(existing).Reference(b => b.Reader).LoadAsync(ct);

        return existing;
    }

    /// <summary>
    /// Удаляет выдачу книги по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Borrows
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity == null)
            return false;

        _context.Borrows.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}