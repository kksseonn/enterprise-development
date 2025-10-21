namespace Library.Domain.Entities;

/// <summary>
/// Запись о выдаче книги читателю
/// </summary>
public class Borrow
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Книга, которую выдали
    /// </summary>
    public required Book Book { get; set; }

    /// <summary>
    /// Читатель, которому выдали
    /// </summary>
    public required Reader Reader { get; set; }

    /// <summary>
    /// Дата выдачи
    /// </summary>
    public required DateOnly BorrowDate { get; set; }

    /// <summary>
    /// Кол-во дней, на которое выдана книга
    /// </summary>
    public required int Days { get; set; }

    /// <summary>
    /// Плановая дата возврата
    /// </summary>
    public DateOnly DueDate => BorrowDate.AddDays(Days);

    /// <summary>
    /// Фактическая дата возврата книги (null, если ещё не сдана)
    /// </summary>
    public DateOnly? ReturnDate { get; set; }
}