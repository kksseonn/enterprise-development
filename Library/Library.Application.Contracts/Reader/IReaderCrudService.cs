using Library.Application.Contracts.Borrow;

namespace Library.Application.Contracts.Reader;

/// <summary>
/// Интерфейс для сервисов чтения данных о читателях
/// </summary>
public interface IReaderCrudService : IApplicationCrudService<ReaderDto, ReaderCrudDto, Guid>
{
    public Task<IReadOnlyList<BorrowDto>> GetBorrows(Guid readerId, CancellationToken ct = default);
}
