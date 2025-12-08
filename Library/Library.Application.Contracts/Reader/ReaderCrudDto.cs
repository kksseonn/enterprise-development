namespace Library.Application.Contracts.Reader;
public record ReaderCrudDto(
    string Surname,
    string Name,
    string? Patronymic,
    string Address,
    string Phone
);

