namespace Library.Application.Contracts.Publisher;

/// <summary>
/// Интерфейс для сервисов чтения данных об издательствах
/// </summary>
public interface IPublisherReadService : IApplicationReadService<PublisherDto, Guid>
{
}