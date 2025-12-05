namespace Library.Application.Contracts.Borrow;

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