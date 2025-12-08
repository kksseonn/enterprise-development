namespace Library.Application.Contracts.Borrow;

/// <summary>
/// DTO для передачи данных о выдаче книги
/// </summary>
/// <param name="Id">Идентификатор выдачи</param>
/// <param name="BookId">Идентификатор книги</param>
/// <param name="ReaderId">Идентификатор читателя</param>
/// <param name="BookTitle">Название книги</param>
/// <param name="ReaderFullName">ФИО читателя</param>
/// <param name="BorrowDate">Дата выдачи</param>
/// <param name="Days">Количество дней выдачи</param>
/// <param name="DueDate">Дата возврата по сроку</param>
/// <param name="ReturnDate">Фактическая дата возврата</param>
public record BorrowDto(
    Guid Id,
    Guid BookId,
    Guid ReaderId,
    string BookTitle,
    string ReaderFullName,
    DateOnly BorrowDate,
    int Days,
    DateOnly DueDate,
    DateOnly? ReturnDate
);