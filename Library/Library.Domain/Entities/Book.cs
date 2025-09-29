namespace Library.Domain.Entities;

/// <summary>
/// книга
/// </summary>
public class Book
{
    /// <summary>
    /// уникальный идентификатор книги
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// инвентарный номер
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// шифр в алфавитном каталоге
    /// </summary>
    public required string CatalogCode { get; set; }

    /// <summary>
    /// название книги
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// авторы
    /// </summary>
    public List<string> Authors { get; set; } = [];

    /// <summary>
    /// вид издания
    /// </summary>
    public required EditionType EditionType { get; set; }

    /// <summary>
    /// издательство
    /// </summary>
    public required Publisher Publisher { get; set; }

    /// <summary>
    /// год издания
    /// </summary>
    public required int Year { get; set; }
}