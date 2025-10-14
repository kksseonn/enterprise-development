using Library.Tests.Fixtures;

namespace Library.Tests;

/// <summary>
/// набор тестов для проверки доменных сущностей библиотект
/// </summary>
public class LibraryDomainTest(DataFixture fixture): IClassFixture<DataFixture>
{
    private readonly DataFixture _fixture = fixture;

    /// <summary>
    /// проверка активных выдач книг по названию
    /// </summary>
    [Fact]
    public void GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder()
    {
        var expectedIds = new List<Guid>
        {
            Guid.Parse("d0000000-0000-0000-0000-000000000007"),
            Guid.Parse("d0000000-0000-0000-0000-000000000008"),
            Guid.Parse("d0000000-0000-0000-0000-000000000001"),
            Guid.Parse("d0000000-0000-0000-0000-000000000002"),
            Guid.Parse("d0000000-0000-0000-0000-000000000006"),
            Guid.Parse("d0000000-0000-0000-0000-000000000005"),
            Guid.Parse("d0000000-0000-0000-0000-000000000009"),
            Guid.Parse("d0000000-0000-0000-0000-000000000010"),
            Guid.Parse("d0000000-0000-0000-0000-000000000003"),
        };

        var resultIds = _fixture.Borrows
            .Where(b => b.ReturnDate is null)
            .OrderBy(b => b.Book!.Title)
            .Select(b => b.Book.Id)
            .ToList();

        Assert.Equal(expectedIds, resultIds);
    }

    /// <summary>
    /// проверка, что возвращается пять самых активных читателей за указанный период
    /// </summary>
    [Fact]
    public void GetTop5Readers_InPeriod_ReturnsCorrectReaders()
    {
        var startDate = new DateOnly(2024, 10, 31);
        var endDate = new DateOnly(2025, 10, 31);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("c0000000-0000-0000-0000-000000000001"),
            Guid.Parse("c0000000-0000-0000-0000-000000000002"),
            Guid.Parse("c0000000-0000-0000-0000-000000000003"),
            Guid.Parse("c0000000-0000-0000-0000-000000000004"),
            Guid.Parse("c0000000-0000-0000-0000-000000000010")
        };

        var topReaders = _fixture.Borrows
            .Where(borrow => borrow.BorrowDate >= startDate && borrow.BorrowDate <= endDate)
            .GroupBy(borrow => borrow.Reader!.Id)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Select(group => group.Key)
            .Take(5)
            .ToList();

        Assert.Equal(expectedIds, topReaders);
    }

    /// <summary>
    /// проверка выборки читетелей, бравших книги на наибольший период времени
    /// </summary>
    [Fact]
    public void GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName()
    {
        var expectedIds = new List<Guid>
        {
            Guid.Parse("c0000000-0000-0000-0000-000000000001"),
            Guid.Parse("c0000000-0000-0000-0000-000000000002"),
            Guid.Parse("c0000000-0000-0000-0000-000000000008")
        };

        var readerMaxDays = _fixture.Borrows
            .GroupBy(borrow => borrow.Reader)
            .Select(g => new
            {
                Reader = g.Key!,
                MaxDays = g.Max(b => b.Days)
            })
            .ToList();

        var globalMax = readerMaxDays.Max(x => x.MaxDays);

        var readersByLongest = readerMaxDays
            .Where(x => x.MaxDays == globalMax)
            .OrderBy(x => x.Reader.Surname)
            .ThenBy(x => x.Reader.Name)
            .ThenBy(x => x.Reader.Patronymic)
            .Select(x => x.Reader.Id)
            .ToList();

        Assert.Equal(expectedIds, readersByLongest);
    }

    /// <summary>
    /// проверка, что возвращается пять наиболее популярных издательств за последний год
    /// </summary>
    [Fact]
    public void GetTop5Publishers_InLastYear_ReturnsExpectedList()
    {
        var today = new DateOnly(2025, 10, 31);
        var oneYearAgo = new DateOnly(2024, 10, 31);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("b0000000-0000-0000-0000-000000000001"),
            Guid.Parse("b0000000-0000-0000-0000-000000000002"),
            Guid.Parse("b0000000-0000-0000-0000-000000000008"),
            Guid.Parse("b0000000-0000-0000-0000-000000000003"),
            Guid.Parse("b0000000-0000-0000-0000-000000000006")
        };

        var topPublishers = _fixture.Borrows
            .Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today)
            .GroupBy(b => b.Book!.Publisher!.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(5)
            .ToList();

        Assert.Equal(expectedIds, topPublishers);
    }

    /// <summary>
    /// проверка, что возвращается пять наименее популярных книг за последний год
    /// </summary>
    [Fact]
    public void GetBottom5Books_InLastYear_ReturnsExpectedBooks()
    {
        var today = new DateOnly(2025, 10, 14);
        var oneYearAgo = new DateOnly(2024, 10, 14);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("d0000000-0000-0000-0000-000000000019"),
            Guid.Parse("d0000000-0000-0000-0000-000000000020"),
            Guid.Parse("d0000000-0000-0000-0000-000000000016"),
            Guid.Parse("d0000000-0000-0000-0000-000000000004"),
            Guid.Parse("d0000000-0000-0000-0000-000000000017")
        };

        var bottomBooks = _fixture.Books
            .GroupJoin(
                _fixture.Borrows.Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today),
                book => book.Id,
                borrow => borrow.Book!.Id,
                (book, borrowGroup) => new { Book = book, BorrowCount = borrowGroup.Count() }
            )
            .OrderBy(x => x.BorrowCount)
            .ThenBy(x => x.Book.Title)
            .Select(x => x.Book.Id)
            .Take(5)
            .ToList();

        Assert.Equal(expectedIds, bottomBooks);
    }
}