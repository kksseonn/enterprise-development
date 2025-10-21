using Library.Domain.Entities;
using Library.Domain.Data;

namespace Library.Tests.Fixtures;

/// <summary>
/// Набор данных для тестов
/// </summary>
public class DataFixture
{
    /// <summary>
    /// Справочник типов изданий
    /// </summary>
    public List<EditionType> EditionTypes { get; }

    /// <summary>
    /// Справочник издательств
    /// </summary>
    public List<Publisher> Publishers { get; }

    /// <summary>
    /// Список читателей
    /// </summary>
    public List<Reader> Readers { get; }

    /// <summary>
    /// Каталог книг
    /// </summary>
    public List<Book> Books { get; }

    /// <summary>
    /// Журнал выдач книг
    /// </summary>
    public List<Borrow> Borrows { get; }

    /// <summary>
    /// Инициализация тестовых данных
    /// </summary>
    public DataFixture()
    {
        EditionTypes = LibraryData.EditionTypes();
        Publishers = LibraryData.Publishers();
        Readers = LibraryData.Readers();
        Books = LibraryData.Books(EditionTypes, Publishers);
        Borrows = LibraryData.Borrows(Books, Readers);
    }

}
