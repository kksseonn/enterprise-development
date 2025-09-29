namespace Library.Domain.Entities;

/// <summary>
/// вид издания
/// </summary>
public class EditionType
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// название издательства
    /// </summary>
    public required string Type { get; set; }
}
