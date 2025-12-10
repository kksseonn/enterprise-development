using Library.Application.Contracts.Book;
using Library.Application.Contracts.Borrow;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с книгами, реализует чтение и CRUD операции
/// </summary>
public class BookService(IRepository<Book> repository, IMapper mapper) : IBookCrudService
{
    /// <summary>
    /// Получает все книги
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг</returns>
    public async Task<IReadOnlyList<BookDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await repository.GetAll(ct);
        return mapper.Map<List<BookDto>>(entities);
    }

    /// <summary>
    /// Получает книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO книги</returns>
    /// <exception cref="KeyNotFoundException">Если книга не найдена</exception>
    public async Task<BookDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await repository.Get(
            id,
            ct,
            includes: [b => b.Publisher!, b => b.EditionType!]
        ) ?? throw new KeyNotFoundException($"Book with ID {id} not found");

        return mapper.Map<BookDto>(entity);
    }

    /// <summary>
    /// Создает новую книгу
    /// </summary>
    /// <param name="dto">DTO книги для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO книги</returns>
    public async Task<BookDto> Create(BookCrudDto dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<Book>(dto);
        var created = await repository.Create(entity, ct);

        return mapper.Map<BookDto>(created);
    }

    /// <summary>
    /// Обновляет существующую книгу
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO книги</returns>
    /// <exception cref="KeyNotFoundException">Если книга не найдена</exception>
    public async Task<BookDto> Update(BookCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"Book with ID {dtoId} not found");

        mapper.Map(dto, existingEntity);

        var updated = await repository.Update(existingEntity, ct);

        return updated == null
            ? throw new KeyNotFoundException($"Book with ID {dtoId} not found during update")
            : mapper.Map<BookDto>(updated);
    }

    /// <summary>
    /// Удаляет книгу по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await repository.Delete(dtoId, ct);
    }

    /// <summary>
    /// Получает все записи о выдачах для конкретной книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO записей о выдачах <see cref="BorrowDto"/></returns>
    /// <exception cref="KeyNotFoundException">Если книга не найдена</exception>
    public async Task<IReadOnlyList<BorrowDto>> GetBorrows(Guid bookId, CancellationToken ct = default)
    {
        var entity = await repository.Get(
            bookId,
            ct,
            includes: b => b.Borrows!
        ) ?? throw new KeyNotFoundException($"Book with ID {bookId} not found");

        return mapper.Map<List<BorrowDto>>(entity.Borrows!);
    }
}