namespace Library.Domain.Entities;

/// <summary>
/// тип издания
/// </summary>
public class EditionType
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// название типа издания
    /// </summary>
    public required string Type { get; set; }
}
