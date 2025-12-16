using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

/// <summary>
/// Тип издания
/// </summary>
[Table("edition_types")]
public class EditionType
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название типа издания
    /// </summary>
    [Column("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Список книг, связанных с определенным типом издания
    /// </summary>
    public virtual List<Book>? Books { get; set; } = new List<Book>();
}
