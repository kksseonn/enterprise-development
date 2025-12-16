using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

/// <summary>
/// Издательство
/// </summary>
[Table("publishers")]
public class Publisher
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название издательства
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Список книг, связанных с издательством
    /// </summary>
    public virtual List<Book>? Books { get; set; } = new List<Book>();
}
