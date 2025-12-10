using Library.Application.Contracts.Book;

namespace Library.Application.Contracts.EditionType;

/// <summary>
/// Интерфейс для сервисов CRUD операций с данными о типах изданий
/// Наследует базовый интерфейс CRUD для <see cref="EditionTypeDto"/> и <see cref="EditionTypeCrudDto"/>
/// </summary>
public interface IEditionTypeCrudService : IApplicationCrudService<EditionTypeDto, EditionTypeCrudDto, Guid>
{
    /// <summary>
    /// Получить все книги, связанные с конкретным типом издания
    /// </summary>
    /// <param name="editionTypeId">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг <see cref="BookDto"/></returns>
    public Task<IReadOnlyList<BookDto>> GetBooks(Guid editionTypeId, CancellationToken ct = default);
}