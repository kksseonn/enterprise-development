using Library.Application.Contracts.Book;

namespace Library.Application.Contracts.Publisher;

/// <summary>
/// Интерфейс для сервисов CRUD операций с данными об издательствах
/// Наследует базовый интерфейс CRUD для <see cref="PublisherDto"/> и <see cref="PublisherCrudDto"/>
/// </summary>
public interface IPublisherCrudService : IApplicationCrudService<PublisherDto, PublisherCrudDto, Guid>
{
    /// <summary>
    /// Получить все книги, связанные с конкретным издательством
    /// </summary>
    /// <param name="publisherId">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг <see cref="BookDto"/></returns>
    public Task<IReadOnlyList<BookDto>> GetBooks(Guid publisherId, CancellationToken ct = default);
}