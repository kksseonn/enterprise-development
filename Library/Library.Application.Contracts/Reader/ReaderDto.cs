namespace Library.Application.Contracts.Reader;

public record ReaderDto(
    Guid Id,
    string Surname,
    string Name,
    string? Patronymic,
    string Address,
    string Phone,
    DateOnly RegistrationDate
);
