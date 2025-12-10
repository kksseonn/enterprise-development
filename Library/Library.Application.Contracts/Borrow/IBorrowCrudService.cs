namespace Library.Application.Contracts.Borrow;

/// <summary>
/// Интерфейс для сервисов CRUD операций с данными о выдаче книг
/// Наследует базовый интерфейс CRUD для <see cref="BorrowDto"/> и <see cref="BorrowCrudDto"/>
/// </summary>
public interface IBorrowCrudService : IApplicationCrudService<BorrowDto, BorrowCrudDto, Guid>
{
}