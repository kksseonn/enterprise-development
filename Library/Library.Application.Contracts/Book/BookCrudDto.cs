namespace Library.Application.Contracts.Book;

/// <summary>
/// DTO для создания и обновления данных о книге
/// </summary>
/// <param name="InventoryNumber">Инвентарный номер</param>
/// <param name="CatalogCode">Код каталога</param>
/// <param name="Title">Название книги</param>
/// <param name="Authors">Список авторов</param>
/// <param name="EditionTypeId">Идентификатор типа издания</param>
/// <param name="PublisherId">Идентификатор издателя</param>
/// <param name="Year">Год издания</param>
public record BookCrudDto(
    int InventoryNumber,
    string CatalogCode,
    string Title,
    List<string> Authors,
    Guid EditionTypeId,
    Guid PublisherId,
    int Year
);