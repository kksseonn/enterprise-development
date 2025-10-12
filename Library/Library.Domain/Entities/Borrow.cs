namespace Library.Domain.Entities;

/// <summary>
/// выдача книги читателю
/// </summary>
public class Borrow
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

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
    public required DateOnly BorrowDate { get; set; }

    /// <summary>
    /// кол-во дней, на которое выдана книга
    /// </summary>
    public required int Days { get; set; }

    /// <summary>
    /// дата сдачи
    /// </summary>
    public DateOnly DueDate => BorrowDate.AddDays(Days); 
    }