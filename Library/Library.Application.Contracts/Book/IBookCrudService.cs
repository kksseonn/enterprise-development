using Library.Application.Contracts.Borrow;

namespace Library.Application.Contracts.Book;

/// <summary>
/// Интерфейс для сервисов CRUD операций с данными о книгах
/// Наследует базовый интерфейс CRUD для <see cref="BookDto"/> и <see cref="BookCrudDto"/>
/// </summary>
public interface IBookCrudService : IApplicationCrudService<BookDto, BookCrudDto, Guid>
{
    /// <summary>
    /// Получает все записи о выдачах для конкретной книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO записей о выдачах <see cref="BorrowDto"/></returns>
    public Task<IReadOnlyList<BorrowDto>> GetBorrows(Guid bookId, CancellationToken ct = default);
}