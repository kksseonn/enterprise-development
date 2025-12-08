namespace Library.Application.Contracts.EditionType;

/// <summary>
/// DTO для передачи данных о типе издания
/// </summary>
/// <param name="Id">Идентификатор типа издания</param>
/// <param name="Type">Название типа издания</param>
public record EditionTypeDto(Guid Id, string Type);