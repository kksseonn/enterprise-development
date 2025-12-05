namespace Library.Domain.Entities;

/// <summary>
/// Книга в библиотечном каталоге
/// </summary>
public class Book
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Инвентарный номер
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Шифр в алфавитном каталоге
    /// </summary>
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Авторы
    /// </summary>
    public List<string> Authors { get; set; } = [];

    /// <summary>
    /// Идентификатор вида издания
    /// </summary>
    public required Guid EditionTypeId { get; set; }

    /// <summary>
    /// Вид издания
    /// </summary>
    public virtual EditionType EditionType { get; set; }

    /// <summary>
    /// Идентификатор издательства
    /// </summary>
    public required Guid PublisherId { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    public virtual Publisher Publisher { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    public required int Year { get; set; }
}