using Library.Application.Contracts.Book;
namespace Library.Application.Contracts.EditionType;

public interface IEditionTypeCrudService : IApplicationCrudService<EditionTypeDto, EditionTypeCrudDto, Guid>
{
    public Task<IReadOnlyList<BookDto>> GetBooks(Guid editionTypeId, CancellationToken ct = default);
}
