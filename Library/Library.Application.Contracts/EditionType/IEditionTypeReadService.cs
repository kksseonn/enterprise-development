namespace Library.Application.Contracts.EditionType;

/// <summary>
/// Интерфейс для сервисов чтения данных о типах изданий
/// </summary>
public interface IEditionTypeReadService : IApplicationReadService<EditionTypeDto, Guid>
{
}