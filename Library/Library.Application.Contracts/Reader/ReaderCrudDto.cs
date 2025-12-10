namespace Library.Application.Contracts.Reader;

/// <summary>
/// DTO для создания и обновления данных о читателе
/// </summary>
/// <param name="Surname">Фамилия</param>
/// <param name="Name">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Address">Адрес</param>
/// <param name="Phone">Телефон</param>
public record ReaderCrudDto(
    string Surname,
    string Name,
    string? Patronymic,
    string Address,
    string Phone
);