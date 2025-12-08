namespace Library.Application.Contracts.Reader;

/// <summary>
/// Интерфейс для сервисов чтения данных о читателях
/// </summary>
public interface IReaderReadService : IApplicationReadService<ReaderDto, Guid>
{
}