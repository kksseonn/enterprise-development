using Library.Domain.Entities;

namespace Library.Domain.Data;

/// <summary>
/// датасет для библиотеки
/// </summary>
public static class LibraryData
{
    /// <summary>
    /// виды изданий
    /// </summary>
    public static EditionType[] EditionTypes() => new[]
    {
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"), Type = "Учебник" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"), Type = "Монография" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"), Type = "Справочник" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"), Type = "Художественная литература" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"), Type = "Фантастика" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"), Type = "Поэзия" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"), Type = "Научная статья" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000008"), Type = "Энциклопедия" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000009"), Type = "Доклад" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000010"), Type = "Комикс" }
    };

    /// <summary>
    /// издательства
    /// </summary>
    public static Publisher[] Publishers() => new[]
    {
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000001"), Name = "Эксмо" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000002"), Name = "АСТ" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000003"), Name = "Просвещение" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000004"), Name = "Наука" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000005"), Name = "Олимп-Бизнес" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000006"), Name = "МИФ" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000007"), Name = "Дрофа" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000008"), Name = "Азбука" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000009"), Name = "Росмэн" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000010"), Name = "Питер" }
    };

    /// <summary>
    /// читатели
    /// </summary>
    public static Reader[] Readers() => new[]
    {
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Иванов", Name = "Иван", Patronymic = "Иванович",  Address = "ул. Ленина, 1", Phone = "+79001234567", RegistrationDate = new DateOnly(2020, 12, 3) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Петров", Name = "Петр", Patronymic = "Петрович", Address = "ул. Гагарина, 2", Phone = "+79012345678", RegistrationDate = new DateOnly(2021, 6, 22) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Сидорова", Name = "Анна", Patronymic = "Павловна", Address = "ул. Чехова, 3", Phone = "+79023456789", RegistrationDate = new DateOnly(2022, 2, 15) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Кузнецов", Name = "Алексей", Patronymic = "Николаевич", Address = "ул. Пушкина, 4", Phone = "+79034567890", RegistrationDate = new DateOnly(2023, 8, 10) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Смирнова", Name = "Ольга", Patronymic = "Викторовна", Address = "ул. Кирова, 5", Phone = "+79045678901", RegistrationDate = new DateOnly(2023, 4, 5) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Попов", Name = "Дмитрий", Patronymic = "Сергеевич", Address = "ул. Мира, 6", Phone = "+79056789012", RegistrationDate = new DateOnly(2022, 11, 14) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Морозова", Name = "Елена", Patronymic = "Андреевна", Address = "ул. Победы, 7", Phone = "+79067890123", RegistrationDate = new DateOnly(2022, 2, 22) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Соколов", Name = "Николай", Patronymic = "Петрович", Address = "ул. Советская, 8", Phone = "+79078901234", RegistrationDate = new DateOnly(2023, 9, 19) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Васильева", Name = "Мария", Patronymic = "Ивановна", Address = "ул. Школьная, 9", Phone = "+79089012345", RegistrationDate = new DateOnly(2024, 5, 3) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Федоров", Name = "Андрей", Patronymic = "Владимирович", Address = "ул. Центральная, 10", Phone = "+79090123456", RegistrationDate = new DateOnly(2024, 1, 17) }
    };

    /// <summary>
    /// книги
    /// </summary>
    public static Book[] Books(EditionType[] editionTypes, Publisher[] publishers) => new[]
    {
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000001"), InventoryNumber = 1001, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], Publisher = publishers[0], Year = 1869 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000002"), InventoryNumber = 1002, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], Publisher = publishers[1], Year = 1866 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000003"), InventoryNumber = 1003, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionType = editionTypes[3], Publisher = publishers[7], Year = 1836 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000004"), InventoryNumber = 1004, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], Publisher = publishers[0], Year = 1877 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000005"), InventoryNumber = 1005, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionType = editionTypes[3], Publisher = publishers[2], Year = 1862 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000006"), InventoryNumber = 1006, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], Publisher = publishers[1], Year = 1869 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000007"), InventoryNumber = 1007, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], Publisher = publishers[5], Year = 1967 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000008"), InventoryNumber = 1008, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionType = editionTypes[5], Publisher = publishers[7], Year = 1833 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000009"), InventoryNumber = 1009, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], Publisher = publishers[6], Year = 1925 },
    new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000010"), InventoryNumber = 1010, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionType = editionTypes[4], Publisher = publishers[8], Year = 1957 }
};

    /// <summary>
    /// выданные книги
    /// </summary>
    public static Borrow[] Borrows(Book[] books, Reader[] readers) => new[]
{
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000001"), Book = books[0], Reader = readers[0], BorrowDate = new DateOnly(2024, 12, 15), Days = 14 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000002"), Book = books[1], Reader = readers[1], BorrowDate = new DateOnly(2025, 1, 10), Days = 30 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000003"), Book = books[2], Reader = readers[2], BorrowDate = new DateOnly(2025, 2, 5), Days = 10 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000004"), Book = books[3], Reader = readers[3], BorrowDate = new DateOnly(2025, 3, 1), Days = 20 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000005"), Book = books[4], Reader = readers[4], BorrowDate = new DateOnly(2025, 3, 15), Days = 7 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000006"), Book = books[5], Reader = readers[5], BorrowDate = new DateOnly(2025, 4, 10), Days = 14 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000007"), Book = books[6], Reader = readers[6], BorrowDate = new DateOnly(2025, 5, 25), Days = 30 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000008"), Book = books[7], Reader = readers[7], BorrowDate = new DateOnly(2025, 6, 14), Days = 15 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000009"), Book = books[8], Reader = readers[8], BorrowDate = new DateOnly(2025, 7, 20), Days = 20 },
    new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000010"), Book = books[9], Reader = readers[9], BorrowDate = new DateOnly(2025, 8, 1), Days = 25 }
};