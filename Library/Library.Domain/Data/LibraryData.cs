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
    public static List<EditionType> SeedEditionTypes() => new()
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
    /// Издательства
    /// </summary>
    public static List<Publisher> SeedPublishers() => new()
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
    /// Читатели
    /// </summary>
    public static List<Reader> SeedReaders() => new()
    {
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"), Surname = "Иванов", Name = "Иван", Patronymic = "Иванович",  Address = "ул. Ленина, 1", Phone = "+79001234567", RegistrationDate = new DateOnly(2023, 12, 3) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000002"), Surname = "Петров", Name = "Петр", Patronymic = "Петрович", Address = "ул. Гагарина, 2", Phone = "+79012345678", RegistrationDate = new DateOnly(2024, 6, 22) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000003"), Surname = "Сидорова", Name = "Анна", Patronymic = "Павловна", Address = "ул. Чехова, 3", Phone = "+79023456789", RegistrationDate = new DateOnly(2025, 2, 15) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000004"), Surname = "Кузнецов", Name = "Алексей", Patronymic = "Николаевич", Address = "ул. Пушкина, 4", Phone = "+79034567890", RegistrationDate = new DateOnly(2023, 8, 10) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000005"), Surname = "Смирнова", Name = "Ольга", Patronymic = "Викторовна", Address = "ул. Кирова, 5", Phone = "+79045678901", RegistrationDate = new DateOnly(2024, 4, 5) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000006"), Surname = "Попов", Name = "Дмитрий", Patronymic = "Сергеевич", Address = "ул. Мира, 6", Phone = "+79056789012", RegistrationDate = new DateOnly(2025, 11, 14) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000007"), Surname = "Морозова", Name = "Елена", Patronymic = "Андреевна", Address = "ул. Победы, 7", Phone = "+79067890123", RegistrationDate = new DateOnly(2023, 2, 22) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000008"), Surname = "Соколов", Name = "Николай", Patronymic = "Петрович", Address = "ул. Советская, 8", Phone = "+79078901234", RegistrationDate = new DateOnly(2024, 9, 19) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000009"), Surname = "Васильева", Name = "Мария", Patronymic = "Ивановна", Address = "ул. Школьная, 9", Phone = "+79089012345", RegistrationDate = new DateOnly(2025, 5, 3) },
        new Reader { Id = Guid.Parse("c0000000-0000-0000-0000-000000000010"), Surname = "Федоров", Name = "Андрей", Patronymic = "Владимирович", Address = "ул. Центральная, 10", Phone = "+79090123456", RegistrationDate = new DateOnly(2023, 1, 17) }
    };

    /// <summary>
    /// Книги
    /// </summary>
    public static List<Book> SeedBooks(List<EditionType> editionTypes, List<Publisher> publishers) => new()
    {
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000001"), InventoryNumber = 1001, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[0].Id, Year = 1869 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000002"), InventoryNumber = 1002, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[0].Id, Year = 1869 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000003"), InventoryNumber = 1003, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[1].Id, Year = 1866 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000004"), InventoryNumber = 1004, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[1].Id, Year = 1866 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000005"), InventoryNumber = 1005, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[7].Id, Year = 1836 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000006"), InventoryNumber = 1006, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[7].Id, Year = 1836 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000007"), InventoryNumber = 1007, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[0].Id, Year = 1877 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000008"), InventoryNumber = 1008, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[0].Id, Year = 1877 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000009"), InventoryNumber = 1009, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[2].Id, Year = 1862 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000010"), InventoryNumber = 1010, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[2].Id, Year = 1862 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000011"), InventoryNumber = 1011, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[1].Id, Year = 1869 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000012"), InventoryNumber = 1012, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionTypeId = editionTypes[3].Id, PublisherId = publishers[1].Id, Year = 1869 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000013"), InventoryNumber = 1013, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionTypeId = editionTypes[4].Id, PublisherId = publishers[5].Id, Year = 1967 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000014"), InventoryNumber = 1014, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionTypeId = editionTypes[4].Id, PublisherId = publishers[5].Id, Year = 1967 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000015"), InventoryNumber = 1015, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionTypeId = editionTypes[5].Id, PublisherId = publishers[7].Id, Year = 1833 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000016"), InventoryNumber = 1016, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionTypeId = editionTypes[5].Id, PublisherId = publishers[7].Id, Year = 1833 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000017"), InventoryNumber = 1017, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionTypeId = editionTypes[4].Id, PublisherId = publishers[6].Id, Year = 1925 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000018"), InventoryNumber = 1018, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionTypeId = editionTypes[4].Id, PublisherId = publishers[6].Id, Year = 1925 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000019"), InventoryNumber = 1019, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionTypeId = editionTypes[4].Id, PublisherId = publishers[8].Id, Year = 1957 },
        new Book { Id = Guid.Parse("d0000000-0000-0000-0000-000000000020"), InventoryNumber = 1020, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionTypeId = editionTypes[4].Id, PublisherId = publishers[8].Id, Year = 1957 }
    };

    /// <summary>
    /// Выданные книги
    /// </summary>
    public static List<Borrow> SeedBorrows(List<Book> books, List<Reader> readers) => new()
    {
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000001"), BookId = books[0].Id, ReaderId = readers[0].Id, BorrowDate = new DateOnly(2024, 1, 10), Days = 14, ReturnDate = new DateOnly(2024, 1, 24) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000002"), BookId = books[1].Id, ReaderId = readers[1].Id, BorrowDate = new DateOnly(2024, 7, 1), Days = 10, ReturnDate = new DateOnly(2024, 7, 11) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000003"), BookId = books[2].Id, ReaderId = readers[3].Id, BorrowDate = new DateOnly(2024, 8, 5), Days = 7, ReturnDate = new DateOnly(2024, 8, 12) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000004"), BookId = books[3].Id, ReaderId = readers[4].Id, BorrowDate = new DateOnly(2024, 5, 1), Days = 20, ReturnDate = new DateOnly(2024, 5, 21) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000005"), BookId = books[4].Id, ReaderId = readers[6].Id, BorrowDate = new DateOnly(2024, 3, 1), Days = 14, ReturnDate = new DateOnly(2024, 3, 15) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000016"), BookId = books[10].Id, ReaderId = readers[9].Id, BorrowDate = new DateOnly(2025, 6, 1), Days = 14, ReturnDate = new DateOnly(2025, 6, 15) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000017"), BookId = books[11].Id, ReaderId = readers[2].Id, BorrowDate = new DateOnly(2025, 3, 1), Days = 10, ReturnDate = new DateOnly(2025, 3, 11) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000018"), BookId = books[12].Id, ReaderId = readers[0].Id, BorrowDate = new DateOnly(2025, 4, 1), Days = 21, ReturnDate = new DateOnly(2025, 4, 22) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000019"), BookId = books[13].Id, ReaderId = readers[6].Id, BorrowDate = new DateOnly(2025, 5, 5), Days = 10, ReturnDate = new DateOnly(2025, 5, 15) },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000020"), BookId = books[14].Id, ReaderId = readers[8].Id, BorrowDate = new DateOnly(2025, 6, 5), Days = 14, ReturnDate = new DateOnly(2025, 6, 19) },

        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000006"), BookId = books[5].Id, ReaderId = readers[0].Id, BorrowDate = new DateOnly(2025, 9, 25), Days = 30 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000007"), BookId = books[6].Id, ReaderId = readers[1].Id, BorrowDate = new DateOnly(2025, 9, 30), Days = 20 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000008"), BookId = books[7].Id, ReaderId = readers[2].Id, BorrowDate = new DateOnly(2025, 10, 1), Days = 15 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000009"), BookId = books[8].Id, ReaderId = readers[3].Id, BorrowDate = new DateOnly(2025, 10, 3), Days = 14 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000010"), BookId = books[9].Id, ReaderId = readers[4].Id, BorrowDate = new DateOnly(2025, 10, 5), Days = 21 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000011"), BookId = books[0].Id, ReaderId = readers[1].Id, BorrowDate = new DateOnly(2025, 9, 28), Days = 30 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000012"), BookId = books[2].Id, ReaderId = readers[3].Id, BorrowDate = new DateOnly(2025, 10, 7), Days = 10 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000013"), BookId = books[4].Id, ReaderId = readers[9].Id, BorrowDate = new DateOnly(2025, 10, 8), Days = 14 },
        new Borrow { Id = Guid.Parse("e0000000-0000-0000-0000-000000000014"), BookId = books[1].Id, ReaderId = readers[7].Id, BorrowDate = new DateOnly(2025, 10, 9), Days = 30 },
    };
}