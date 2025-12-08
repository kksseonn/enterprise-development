namespace Library.Application.Contracts.Borrow;

/// <summary>
/// Интерфейс для сервисов чтения данных о выдачах книг
/// </summary>
public interface IBorrowReadService : IApplicationReadService<BorrowDto, Guid>
{
}