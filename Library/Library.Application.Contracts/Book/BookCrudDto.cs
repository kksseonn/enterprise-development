namespace Library.Application.Contracts.Book;

public record BookCrudDto(
    int InventoryNumber,
    string CatalogCode,
    string Title,
    List<string> Authors,
    Guid EditionTypeId,
    Guid PublisherId,
    int Year
);