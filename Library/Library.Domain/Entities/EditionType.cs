namespace Library.Domain.Entities;

/// <summary>
/// Тип издания
/// </summary>
public class EditionType
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название типа издания
    /// </summary>
    public required string Type { get; set; }
}
