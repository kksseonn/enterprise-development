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
    public static EditionType[] SeedEditionTypes() =>
    [
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
    ];

    /// <summary>
    /// Издательства
    /// </summary>
    public static Publisher[] SeedPublishers() =>
    [
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
    ];

    /// <summary>
    /// Читатели
    /// </summary>
    public static Reader[] SeedReaders() =>
    [
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
    ];

    /// <summary>
    /// Книги
    /// </summary>
    public static Book[] SeedBooks(EditionType[] editionTypes, Publisher[] publishers) =>
    [
        new Book { InventoryNumber = 1001, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[0], PublisherId = publishers[0].Id, Year = 1869 },
        new Book { InventoryNumber = 1002, CatalogCode = "A-01", Title = "Война и мир", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[0], PublisherId = publishers[0].Id, Year = 1869 },
        new Book { InventoryNumber = 1003, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[1], PublisherId = publishers[1].Id, Year = 1866 },
        new Book { InventoryNumber = 1004, CatalogCode = "A-02", Title = "Преступление и наказание", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[1], PublisherId = publishers[1].Id, Year = 1866 },
        new Book { InventoryNumber = 1005, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[7], PublisherId = publishers[7].Id, Year = 1836 },
        new Book { InventoryNumber = 1006, CatalogCode = "B-01", Title = "Капитанская дочка", Authors = ["А.С. Пушкин"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[7], PublisherId = publishers[7].Id, Year = 1836 },
        new Book { InventoryNumber = 1007, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[0], PublisherId = publishers[0].Id, Year = 1877 },
        new Book { InventoryNumber = 1008, CatalogCode = "B-02", Title = "Анна Каренина", Authors = ["Л.Н. Толстой"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[0], PublisherId = publishers[0].Id, Year = 1877 },
        new Book { InventoryNumber = 1009, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[2], PublisherId = publishers[2].Id, Year = 1862 },
        new Book { InventoryNumber = 1010, CatalogCode = "C-01", Title = "Отцы и дети", Authors = ["И.С. Тургенев"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[2], PublisherId = publishers[2].Id, Year = 1862 },
        new Book { InventoryNumber = 1011, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[1], PublisherId = publishers[1].Id, Year = 1869 },
        new Book { InventoryNumber = 1012, CatalogCode = "C-02", Title = "Идиот", Authors = ["Ф.М. Достоевский"], EditionType = editionTypes[3], EditionTypeId = editionTypes[3].Id, Publisher = publishers[1], PublisherId = publishers[1].Id, Year = 1869 },
        new Book { InventoryNumber = 1013, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], EditionTypeId = editionTypes[4].Id, Publisher = publishers[5], PublisherId = publishers[5].Id, Year = 1967 },
        new Book { InventoryNumber = 1014, CatalogCode = "D-01", Title = "Мастер и Маргарита", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], EditionTypeId = editionTypes[4].Id, Publisher = publishers[5], PublisherId = publishers[5].Id, Year = 1967 },
        new Book { InventoryNumber = 1015, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionType = editionTypes[5], EditionTypeId = editionTypes[5].Id, Publisher = publishers[7], PublisherId = publishers[7].Id, Year = 1833 },
        new Book { InventoryNumber = 1016, CatalogCode = "D-02", Title = "Евгений Онегин", Authors = ["А.С. Пушкин"], EditionType = editionTypes[5], EditionTypeId = editionTypes[5].Id, Publisher = publishers[7], PublisherId = publishers[7].Id, Year = 1833 },
        new Book { InventoryNumber = 1017, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], EditionTypeId = editionTypes[4].Id, Publisher = publishers[6], PublisherId = publishers[6].Id, Year = 1925 },
        new Book { InventoryNumber = 1018, CatalogCode = "E-01", Title = "Собачье сердце", Authors = ["М.А. Булгаков"], EditionType = editionTypes[4], EditionTypeId = editionTypes[4].Id, Publisher = publishers[6], PublisherId = publishers[6].Id, Year = 1925 },
        new Book { InventoryNumber = 1019, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionType = editionTypes[4], EditionTypeId = editionTypes[4].Id, Publisher = publishers[8], PublisherId = publishers[8].Id, Year = 1957 },
        new Book { InventoryNumber = 1020, CatalogCode = "E-02", Title = "Доктор Живаго", Authors = ["Б.Л. Пастернак"], EditionType = editionTypes[4], EditionTypeId = editionTypes[4].Id, Publisher = publishers[8], PublisherId = publishers[8].Id, Year = 1957 }
    ];

    /// <summary>
    /// Выданные книги
    /// </summary>
    public static Borrow[] SeedBorrows(Book[] books, Reader[] readers) =>
    [
        new Borrow { Book = books[0], BookId = books[0].Id, Reader = readers[0], ReaderId = readers[0].Id, BorrowDate = new DateOnly(2024, 1, 10), Days = 14, ReturnDate = new DateOnly(2024, 1, 24) },
        new Borrow { Book = books[1], BookId = books[1].Id, Reader = readers[1], ReaderId = readers[1].Id, BorrowDate = new DateOnly(2024, 7, 1), Days = 10, ReturnDate = new DateOnly(2024, 7, 11) },
        new Borrow { Book = books[2], BookId = books[2].Id, Reader = readers[3], ReaderId = readers[3].Id, BorrowDate = new DateOnly(2024, 8, 5), Days = 7, ReturnDate = new DateOnly(2024, 8, 12) },
        new Borrow { Book = books[3], BookId = books[3].Id, Reader = readers[4], ReaderId = readers[4].Id, BorrowDate = new DateOnly(2024, 5, 1), Days = 20, ReturnDate = new DateOnly(2024, 5, 21) },
        new Borrow { Book = books[4], BookId = books[4].Id, Reader = readers[6], ReaderId = readers[6].Id, BorrowDate = new DateOnly(2024, 3, 1), Days = 14, ReturnDate = new DateOnly(2024, 3, 15) },
        new Borrow { Book = books[10], BookId = books[10].Id, Reader = readers[9], ReaderId = readers[9].Id, BorrowDate = new DateOnly(2025, 6, 1), Days = 14, ReturnDate = new DateOnly(2025, 6, 15) },
        new Borrow { Book = books[11], BookId = books[11].Id, Reader = readers[2], ReaderId = readers[2].Id, BorrowDate = new DateOnly(2025, 3, 1), Days = 10, ReturnDate = new DateOnly(2025, 3, 11) },
        new Borrow { Book = books[12], BookId = books[12].Id, Reader = readers[0], ReaderId = readers[0].Id, BorrowDate = new DateOnly(2025, 4, 1), Days = 21, ReturnDate = new DateOnly(2025, 4, 22) },
        new Borrow { Book = books[13], BookId = books[13].Id, Reader = readers[6], ReaderId = readers[6].Id, BorrowDate = new DateOnly(2025, 5, 5), Days = 10, ReturnDate = new DateOnly(2025, 5, 15) },
        new Borrow { Book = books[14], BookId = books[14].Id, Reader = readers[8], ReaderId = readers[8].Id, BorrowDate = new DateOnly(2025, 6, 5), Days = 14, ReturnDate = new DateOnly(2025, 6, 19) },

        new Borrow { Book = books[5], BookId = books[5].Id, Reader = readers[0], ReaderId = readers[0].Id, BorrowDate = new DateOnly(2025, 9, 25), Days = 30 },
        new Borrow { Book = books[6], BookId = books[6].Id, Reader = readers[1], ReaderId = readers[1].Id, BorrowDate = new DateOnly(2025, 9, 30), Days = 20 },
        new Borrow { Book = books[7], BookId = books[7].Id, Reader = readers[2], ReaderId = readers[2].Id, BorrowDate = new DateOnly(2025, 10, 1), Days = 15 },
        new Borrow { Book = books[8], BookId = books[8].Id, Reader = readers[3], ReaderId = readers[3].Id, BorrowDate = new DateOnly(2025, 10, 3), Days = 14 },
        new Borrow { Book = books[9], BookId = books[9].Id, Reader = readers[4], ReaderId = readers[4].Id, BorrowDate = new DateOnly(2025, 10, 5), Days = 21 },
        new Borrow { Book = books[0], BookId = books[0].Id, Reader = readers[1], ReaderId = readers[1].Id, BorrowDate = new DateOnly(2025, 9, 28), Days = 30 },
        new Borrow { Book = books[2], BookId = books[2].Id, Reader = readers[3], ReaderId = readers[3].Id, BorrowDate = new DateOnly(2025, 10, 7), Days = 10 },
        new Borrow { Book = books[4], BookId = books[4].Id, Reader = readers[9], ReaderId = readers[9].Id, BorrowDate = new DateOnly(2025, 10, 8), Days = 14 },
        new Borrow { Book = books[1], BookId = books[1].Id, Reader = readers[7], ReaderId = readers[7].Id, BorrowDate = new DateOnly(2025, 10, 9), Days = 30 },
    ];
}