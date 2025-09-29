namespace Library.Domain.Entities;

/// <summary>
/// выдача книги читателю
/// </summary>
public class Borrow
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// книга, которую выдали
    /// </summary>
    public required Book Book { get; set; }

    /// <summary>
    /// читатель, которому выдали
    /// </summary>
    public required Reader Reader { get; set; }

    /// <summary>
    /// дата выдачи
    /// </summary>
    public required DateTime BorrowDate { get; set; }

    /// <summary>
    /// кол-во дней, на которое выдана книга
    /// </summary>
    public required int Days { get; set; }
}