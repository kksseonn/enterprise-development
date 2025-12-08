namespace Library.Domain.Entities;

/// <summary>
/// Издательство
/// </summary>
public class Publisher
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название издательства
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Список книг, связанных с издательством
    /// </summary>
    public virtual List<Book>? Books { get; set; } = new List<Book>();
}
