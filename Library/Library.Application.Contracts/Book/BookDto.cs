namespace Library.Application.Contracts.Book;

/// <summary>
/// DTO для передачи данных о книге
/// </summary>
/// <param name="Id">Идентификатор книги</param>
/// <param name="InventoryNumber">Инвентарный номер</param>
/// <param name="CatalogCode">Код каталога</param>
/// <param name="Title">Название книги</param>
/// <param name="Authors">Список авторов</param>
/// <param name="EditionTypeId">Идентификатор типа издания</param>
/// <param name="PublisherId">Идентификатор издателя</param>
/// <param name="EditionTypeName">Название типа издания</param>
/// <param name="PublisherName">Название издателя</param>
/// <param name="Year">Год издания</param>
public record BookDto(
    Guid Id,
    int InventoryNumber,
    string CatalogCode,
    string Title,
    List<string> Authors,
    Guid EditionTypeId,
    Guid PublisherId,
    string EditionTypeName,
    string PublisherName,
    int Year
);