using Library.Domain.Entities;

namespace Library.Domain.Data;

/// <summary>
/// Набор исходных данных для доменной модели библиотеки
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Виды изданий
    /// </summary>
    public static EditionType[] SeedEditionTypes() => new[]
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
    public static Publisher[] SeedPublishers() => new[]
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
    public static Reader[] SeedReaders() => new[]
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

    /// <summary>
    /// Книги
    /// </summary>
    public static Book[] SeedBooks(EditionType[] editionTypes, Publisher[] publishers) => new[]
    {
        new Book { InventoryNumber = 1001, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], Publisher = publishers[0], Year = 1869 },
        new Book { InventoryNumber = 1002, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], Publisher = publishers[0], Year = 1869 },
        new Book { InventoryNumber = 1003, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], Publisher = publishers[1], Year = 1866 },
        new Book { InventoryNumber = 1004, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], Publisher = publishers[1], Year = 1866 },
        new Book { InventoryNumber = 1005, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionType = editionTypes[3], Publisher = publishers[7], Year = 1836 },
        new Book { InventoryNumber = 1006, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionType = editionTypes[3], Publisher = publishers[7], Year = 1836 },
        new Book { InventoryNumber = 1007, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], Publisher = publishers[0], Year = 1877 },
        new Book { InventoryNumber = 1008, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], Publisher = publishers[0], Year = 1877 },
        new Book { InventoryNumber = 1009, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionType = editionTypes[3], Publisher = publishers[2], Year = 1862 },
        new Book { InventoryNumber = 1010, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionType = editionTypes[3], Publisher = publishers[2], Year = 1862 },
        new Book { InventoryNumber = 1011, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], Publisher = publishers[1], Year = 1869 },
        new Book { InventoryNumber = 1012, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], Publisher = publishers[1], Year = 1869 },
        new Book { InventoryNumber = 1013, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], Publisher = publishers[5], Year = 1967 },
        new Book { InventoryNumber = 1014, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], Publisher = publishers[5], Year = 1967 },
        new Book { InventoryNumber = 1015, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionType = editionTypes[5], Publisher = publishers[7], Year = 1833 },
        new Book { InventoryNumber = 1016, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionType = editionTypes[5], Publisher = publishers[7], Year = 1833 },
        new Book { InventoryNumber = 1017, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], Publisher = publishers[6], Year = 1925 },
        new Book { InventoryNumber = 1018, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], Publisher = publishers[6], Year = 1925 },
        new Book { InventoryNumber = 1019, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionType = editionTypes[4], Publisher = publishers[8], Year = 1957 },
        new Book { InventoryNumber = 1020, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionType = editionTypes[4], Publisher = publishers[8], Year = 1957 }
    };
}