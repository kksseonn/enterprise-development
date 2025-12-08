namespace Library.Application.Contracts.Borrow;

public record BorrowCrudDto(
    Guid BookId,
    Guid ReaderId,
    DateOnly BorrowDate,
    int Days,
    DateOnly? ReturnDate
);