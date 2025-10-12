using Library.Domain.Entities;
using Library.Domain.Data;

namespace Library.Tests.Fixtures;
public class DataFixture
{
    public EditionType[] EditionTypes { get; }
    public Publisher[] Publishers { get; }
    public Reader[] Readers { get; }
    public Book[] Books { get; }
    public Borrow[] Borrows { get; }

    public DataFixture()
    {
        EditionTypes = LibraryData.EditionTypes();
        Publishers = LibraryData.Publishers();
        Readers = LibraryData.Readers();
        Books = LibraryData.Books(EditionTypes, Publishers);
        Borrows = LibraryData.Borrows(Books, Readers);
    }

}
