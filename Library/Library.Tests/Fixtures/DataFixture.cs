using Library.Domain.Entities;
using Library.Domain.Data;

namespace Library.Tests.Fixtures;

/// <summary>
/// набор данных для тестов
/// </summary>
public class DataFixture
{
    /// <summary>
    /// справочник видов изданий
    /// </summary>
    public EditionType[] EditionTypes { get; }

    /// <summary>
    /// справочник издательств
    /// </summary>
    public Publisher[] Publishers { get; }

    /// <summary>
    /// список читателей
    /// </summary>
    public Reader[] Readers { get; }

    /// <summary>
    /// каталог книг
    /// </summary>
    public Book[] Books { get; }

    /// <summary>
    /// журнал выдач книг
    /// </summary>
    public Borrow[] Borrows { get; }

    /// <summary>
    /// инициализация тестовых данных
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
