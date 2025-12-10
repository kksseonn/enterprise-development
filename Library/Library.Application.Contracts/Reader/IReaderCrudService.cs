using Library.Application.Contracts.Borrow;

namespace Library.Application.Contracts.Reader;

/// <summary>
/// Интерфейс для сервисов CRUD операций с данными о читателях
/// Наследует базовый интерфейс CRUD для <see cref="ReaderDto"/> и <see cref="ReaderCrudDto"/>
/// </summary>
public interface IReaderCrudService : IApplicationCrudService<ReaderDto, ReaderCrudDto, Guid>
{
    /// <summary>
    /// Получает все записи о выдачах книг для конкретного читателя
    /// </summary>
    /// <param name="readerId">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO записей о выдачах <see cref="BorrowDto"/></returns>
    public Task<IReadOnlyList<BorrowDto>> GetBorrows(Guid readerId, CancellationToken ct = default);
}