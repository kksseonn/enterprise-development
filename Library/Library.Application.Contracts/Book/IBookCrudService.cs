using Library.Application.Contracts.Borrow;

namespace Library.Application.Contracts.Book;

/// <summary>
/// Интерфейс для сервисов чтения данных о читателях
/// </summary>
public interface IBookCrudService : IApplicationCrudService<BookDto, BookCrudDto, Guid>
{
    public Task<IReadOnlyList<BorrowDto>> GetBorrows(Guid bookId, CancellationToken ct = default);
}
