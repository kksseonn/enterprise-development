namespace Library.Domain.Entities;

/// <summary>
/// издательство
/// </summary>
public class Publisher
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// название издательства
    /// </summary>
    public required string Name { get; set; }
}
