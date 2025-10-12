namespace Library.Domain.Entities;

/// <summary>
/// вид издания
/// </summary>
public class EditionType
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// название издательства
    /// </summary>
    public required string Type { get; set; }
}
