namespace Library.Domain.Entities;

/// <summary>
/// книга
/// </summary>
public class Book
{
    public int Id { get; set; }

    /// <summary>
    /// инвентарный номер
    /// </summary>
    public int InventoryNumber { get; set; }

    /// <summary>
    /// шифр в алфавитном каталоге
    /// </summary>
    public string CatalogCode { get; set; } = string.Empty;

    /// <summary>
    /// название книги
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// авторы
    /// </summary>
    public List<string> Authors { get; set; } = new();

    /// <summary>
    /// вид издания
    /// </summary>
    public EditionType EditionType { get; set; } = new();

    /// <summary>
    /// издательство
    /// </summary>
    public Publisher Publisher { get; set; } = new();

    /// <summary>
    /// год издания
    /// </summary>
    public int Year { get; set; }
}