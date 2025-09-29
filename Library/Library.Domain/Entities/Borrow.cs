namespace Library.Domain.Entities;

/// <summary>
/// выдача книги читателю
/// </summary>
public class Borrow
{
    public int Id { get; set; }

    /// <summary>
    /// книга, которую выдали
    /// </summary>
    public Book Book { get; set; } = new();

    /// <summary>
    /// читатель, которому выдали
    /// </summary>
    public Reader Reader { get; set; } = new();

    /// <summary>
    /// дата выдачи
    /// </summary>
    public DateTime BorrowDate { get; set; }

    /// <summary>
    /// кол-во дней, на которое выдана книга
    /// </summary>
    public int Days { get; set; }
}