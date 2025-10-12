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
}
