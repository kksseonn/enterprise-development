using Library.Domain.Entities;

namespace Library.Domain.Data;

/// <summary>
/// Набор исходных данных для доменной модели библиотеки
/// </summary>
public class LibraryData
{
    /// <summary>
    /// Виды изданий
    /// </summary>
    public static EditionType[] EditionTypes() => new[]
    {
        new EditionType { Type = "Учебник" },
        new EditionType { Type = "Монография" },
        new EditionType { Type = "Справочник" },
        new EditionType { Type = "Художественная литература" },
        new EditionType { Type = "Фантастика" },
        new EditionType { Type = "Поэзия" },
        new EditionType { Type = "Научная статья" },
        new EditionType { Type = "Энциклопедия" },
        new EditionType { Type = "Доклад" },
        new EditionType { Type = "Комикс" }
    };

    /// <summary>
    /// Издательства
    /// </summary>
    public static Publisher[] Publishers() => new[]
    {
        new Publisher { Name = "Эксмо" },
        new Publisher { Name = "АСТ" },
        new Publisher { Name = "Просвещение" },
        new Publisher { Name = "Наука" },
        new Publisher { Name = "Олимп-Бизнес" },
        new Publisher { Name = "МИФ" },
        new Publisher { Name = "Дрофа" },
        new Publisher { Name = "Азбука" },
        new Publisher { Name = "Росмэн" },
        new Publisher { Name = "Питер" }
    };

    /// <summary>
    /// Читатели
    /// </summary>
    public static Reader[] Readers() => new[]
    {
        new Reader { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович",  Address = "ул. Ленина, 1", Phone = "+79001234567", RegistrationDate = new DateOnly(2023, 12, 3) },
        new Reader { Surname = "Петров", Name = "Петр", Patronymic = "Петрович", Address = "ул. Гагарина, 2", Phone = "+79012345678", RegistrationDate = new DateOnly(2024, 6, 22) },
        new Reader { Surname = "Сидорова", Name = "Анна", Patronymic = "Павловна", Address = "ул. Чехова, 3", Phone = "+79023456789", RegistrationDate = new DateOnly(2025, 2, 15) },
        new Reader { Surname = "Кузнецов", Name = "Алексей", Patronymic = "Николаевич", Address = "ул. Пушкина, 4", Phone = "+79034567890", RegistrationDate = new DateOnly(2023, 8, 10) },
        new Reader { Surname = "Смирнова", Name = "Ольга", Patronymic = "Викторовна", Address = "ул. Кирова, 5", Phone = "+79045678901", RegistrationDate = new DateOnly(2024, 4, 5) },
        new Reader { Surname = "Попов", Name = "Дмитрий", Patronymic = "Сергеевич", Address = "ул. Мира, 6", Phone = "+79056789012", RegistrationDate = new DateOnly(2025, 11, 14) },
        new Reader { Surname = "Морозова", Name = "Елена", Patronymic = "Андреевна", Address = "ул. Победы, 7", Phone = "+79067890123", RegistrationDate = new DateOnly(2023, 2, 22) },
        new Reader { Surname = "Соколов", Name = "Николай", Patronymic = "Петрович", Address = "ул. Советская, 8", Phone = "+79078901234", RegistrationDate = new DateOnly(2024, 9, 19) },
        new Reader { Surname = "Васильева", Name = "Мария", Patronymic = "Ивановна", Address = "ул. Школьная, 9", Phone = "+79089012345", RegistrationDate = new DateOnly(2025, 5, 3) },
        new Reader { Surname = "Федоров", Name = "Андрей", Patronymic = "Владимирович", Address = "ул. Центральная, 10", Phone = "+79090123456", RegistrationDate = new DateOnly(2023, 1, 17) }
    };
}