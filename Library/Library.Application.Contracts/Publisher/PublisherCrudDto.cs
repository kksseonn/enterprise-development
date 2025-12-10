namespace Library.Application.Contracts.Publisher;

/// <summary>
/// DTO для создания и обновления данных об издательстве
/// </summary>
/// <param name="Name">Название издательства</param>
public record PublisherCrudDto(string Name);