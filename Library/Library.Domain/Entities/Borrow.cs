using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

/// <summary>
/// Запись о выдаче книги читателю
/// </summary>
[Table("borrows")]
public class Borrow
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор книги, которую выдали
    /// </summary>
    [Column("book_id")]
    public required Guid BookId { get; set; }

    /// <summary>
    /// Книга, которую выдали
    /// </summary>
    public virtual Book? Book { get; set; }

    /// <summary>
    /// Идентификатор читателя, которому выдали
    /// </summary>
    [Column("reader_id")]
    public required Guid ReaderId { get; set; }

    /// <summary>
    /// Читатель, которому выдали
    /// </summary>
    public virtual Reader? Reader { get; set; }

    /// <summary>
    /// Дата выдачи
    /// </summary>
    [Column("borrow_date")]
    public required DateOnly BorrowDate { get; set; }

    /// <summary>
    /// Кол-во дней, на которое выдана книга
    /// </summary>
    [Column("days")]
    public required int Days { get; set; }

    /// <summary>
    /// Плановая дата возврата
    /// </summary>
    [Column("due_date")]
    public DateOnly DueDate { get; set; }

    /// <summary>
    /// Фактическая дата возврата книги (null, если ещё не сдана)
    /// </summary>
    [Column("return_date")]
    public DateOnly? ReturnDate { get; set; }
}