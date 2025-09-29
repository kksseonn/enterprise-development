namespace Library.Domain.Entities;

/// <summary>
/// издательство
/// </summary>
public class Publisher
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// название издательства
    /// </summary>
    public required string Name { get; set; }
}
