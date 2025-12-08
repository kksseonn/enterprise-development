namespace Library.Application.Contracts.Publisher;

/// <summary>
/// DTO для передачи данных об издательстве
/// </summary>
/// <param name="Id">Идентификатор издательства</param>
/// <param name="Name">Название издательства</param>
public record PublisherDto(Guid Id, string Name);