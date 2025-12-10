namespace Library.Application.Contracts.Borrow;

/// <summary>
/// DTO для создания новой записи о выдаче книги или обновления существующей
/// При создании <paramref name="ReturnDate"/> должен быть <see langword="null"/>
/// При возврате книги (обновлении) должно быть указано значение <paramref name="ReturnDate"/>
/// </summary>
/// <param name="BookId">Идентификатор книги, которая выдается</param>
/// <param name="ReaderId">Идентификатор читателя, который берет книгу</param>
/// <param name="BorrowDate">Дата выдачи книги</param>
/// <param name="Days">Планируемое количество дней для выдачи</param>
/// <param name="ReturnDate">Фактическая дата возврата (может быть null, если книга еще не возвращена)</param>
public record BorrowCrudDto(
    Guid BookId,
    Guid ReaderId,
    DateOnly BorrowDate,
    int Days,
    DateOnly? ReturnDate
);