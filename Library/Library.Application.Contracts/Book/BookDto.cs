namespace Library.Application.Contracts.Book;

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