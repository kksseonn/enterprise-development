namespace Library.Application.Contracts.Book;

/// <summary>
/// Интерфейс для сервисов чтения данных о книгах
/// </summary>
public interface IBookReadService : IApplicationReadService<BookDto, Guid>
{
}