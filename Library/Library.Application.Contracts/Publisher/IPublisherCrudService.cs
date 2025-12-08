using Library.Application.Contracts.Book;

namespace Library.Application.Contracts.Publisher;

/// <summary>
/// Интерфейс для сервисов чтения данных об издательствах
/// </summary>
public interface IPublisherCrudService : IApplicationCrudService<PublisherDto, PublisherCrudDto, Guid>
{
    /// <summary>
    /// Получить все книги, связанные с конкретным издателем
    /// </summary>
    /// <param name="publisherId">Идентификатор издателя</param>
    /// <returns>Список DTO книг</returns>
    public Task<IReadOnlyList<BookDto>> GetBooks(Guid publisherId, CancellationToken ct = default);
}
