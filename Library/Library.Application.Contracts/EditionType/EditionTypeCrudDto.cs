namespace Library.Application.Contracts.EditionType;

/// <summary>
/// DTO для создания и обновления данных о типе издания
/// </summary>
/// <param name="Type">Название типа издания</param>
public record EditionTypeCrudDto(string Type);