using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

/// <summary>
/// Книга в библиотечном каталоге
/// </summary>
[Table("books")]
public class Book
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Инвентарный номер
    /// </summary>
    [Column("inventory_number")]
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Шифр в алфавитном каталоге
    /// </summary>
    [Column("catalog_code")]
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    [Column("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Авторы
    /// </summary>
    [Column("authors")]
    public List<string> Authors { get; set; } = [];

    /// <summary>
    /// Идентификатор вида издания
    /// </summary>
    [Column("edition_type_id")]
    public required Guid EditionTypeId { get; set; }

    /// <summary>
    /// Вид издания
    /// </summary>
    public virtual EditionType? EditionType { get; set; }

    /// <summary>
    /// Идентификатор издательства
    /// </summary>
    [Column("publisher_id")]
    public required Guid PublisherId { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    public virtual Publisher? Publisher { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    [Column("year")]
    public required int Year { get; set; }

    /// <summary>
    /// Записи о выдаче данной книги
    /// </summary>
    public virtual List<Borrow>? Borrows { get; set; } = new List<Borrow>();
}