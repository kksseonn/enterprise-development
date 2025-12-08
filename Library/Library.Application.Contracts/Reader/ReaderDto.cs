namespace Library.Application.Contracts.Reader;

/// <summary>
/// DTO для передачи данных о читателе
/// </summary>
/// <param name="Id">Идентификатор читателя</param>
/// <param name="Surname">Фамилия</param>
/// <param name="Name">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Address">Адрес</param>
/// <param name="Phone">Телефон</param>
/// <param name="RegistrationDate">Дата регистрации</param>
public record ReaderDto(
    Guid Id,
    string Surname,
    string Name,
    string? Patronymic,
    string Address,
    string Phone,
    DateOnly RegistrationDate
);