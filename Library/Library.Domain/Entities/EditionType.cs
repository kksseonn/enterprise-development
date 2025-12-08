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

    /// <summary>
    /// Список книг, связанных с определенным типом издания
    /// </summary>
    public virtual List<Book>? Books { get; set; } = new List<Book>();
}
