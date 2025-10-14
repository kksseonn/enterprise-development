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

        var resultIds =
            (from borrow in _fixture.Borrows
             where borrow.ReturnDate is null
             orderby borrow.Book!.Title
             select borrow.Book.Id)
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

        var topReaders =
            (from borrow in _fixture.Borrows
             where borrow.BorrowDate >= startDate && borrow.BorrowDate <= endDate
             group borrow by borrow.Reader!.Id into readerGroup
             orderby readerGroup.Count() descending, readerGroup.Key
             select readerGroup.Key)
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

        var readerMaxDays =
            (from borrow in _fixture.Borrows
             group borrow by borrow.Reader into readerGroup
             select new
             {
                 Reader = readerGroup.Key!,
                 MaxDays = readerGroup.Max(b => b.Days)
             })
            .ToList();

        var globalMax = readerMaxDays.Max(x => x.MaxDays);

        var readersByLongest =
            (from r in readerMaxDays
             where r.MaxDays == globalMax
             orderby r.Reader.Surname, r.Reader.Name, r.Reader.Patronymic
             select r.Reader.Id)
            .ToList();

        Assert.Equal(expectedIds, readersByLongest);
    }

    /// <summary>
    /// проверка, что возвращается пять наиболее популярных издательств за последний год
    /// </summary>
    [Fact]
    public void GetTop5Publishers_InLastYear_ReturnsExpectedList()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var oneYearAgo = today.AddYears(-1);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("b0000000-0000-0000-0000-000000000001"),
            Guid.Parse("b0000000-0000-0000-0000-000000000002"),
            Guid.Parse("b0000000-0000-0000-0000-000000000008"),
            Guid.Parse("b0000000-0000-0000-0000-000000000003"),
            Guid.Parse("b0000000-0000-0000-0000-000000000006")
        };

        var topPublishers =
            (from borrow in _fixture.Borrows
             where borrow.BorrowDate >= oneYearAgo && borrow.BorrowDate <= today
             group borrow by borrow.Book!.Publisher!.Id into publisherGroup
             orderby publisherGroup.Count() descending, publisherGroup.Key
             select publisherGroup.Key)
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
        var today = DateOnly.FromDateTime(DateTime.Today);
        var oneYearAgo = today.AddYears(-1);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("d0000000-0000-0000-0000-000000000007"),
            Guid.Parse("d0000000-0000-0000-0000-000000000008"),
            Guid.Parse("d0000000-0000-0000-0000-000000000001"),
            Guid.Parse("d0000000-0000-0000-0000-000000000002"),
            Guid.Parse("d0000000-0000-0000-0000-000000000015")
        };

        var bottomBooks =
            (from borrow in _fixture.Borrows
             where borrow.BorrowDate >= oneYearAgo && borrow.BorrowDate <= today
             group borrow by borrow.Book!.Id into bookGroup
             orderby bookGroup.Count(),
                     (from b in _fixture.Books
                      where b.Id == bookGroup.Key
                      select b.Title).FirstOrDefault()
             select bookGroup.Key)
            .Take(5)
            .ToList();

        Assert.Equal(expectedIds, bottomBooks);
    }
}