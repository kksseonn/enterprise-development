using Library.Application.Service;
using Library.Tests.Fixtures;

namespace Library.Tests;

/// <summary>
/// Набор тестов для проверки доменных сущностей библиотеки
/// </summary>
public class LibraryDomainTest(AnalyticsFixture fixture) : IClassFixture<AnalyticsFixture>
{
    private readonly AnalyticsService _service = fixture.Service;

    /// <summary>
    /// Проверка активных выдач книг по названию
    /// </summary>
    [Fact]
    public async void GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder()
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
            Guid.Parse("d0000000-0000-0000-0000-000000000003")
        };

        var resultIds = await _service.GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder();

        Assert.Equal(expectedIds, resultIds);
    }

    /// <summary>
    /// Проверка, что возвращается пять самых активных читателей за указанный период
    /// </summary>
    [Fact]
    public async void GetTop5Readers_InPeriod_ReturnsCorrectReaders()
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

        var topReaders = await _service.GetTop5Readers_InPeriod_ReturnsCorrectReaders(startDate, endDate);

        Assert.Equal(expectedIds, topReaders);
    }

    /// <summary>
    /// Проверка выборки читателей, бравших книги на наибольший период времени
    /// </summary>
    [Fact]
    public async void GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName()
    {
        var expectedIds = new List<Guid>
        {
            Guid.Parse("c0000000-0000-0000-0000-000000000001"),
            Guid.Parse("c0000000-0000-0000-0000-000000000002"),
            Guid.Parse("c0000000-0000-0000-0000-000000000008")
        };

        var readersByLongest = await _service.GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName();

        Assert.Equal(expectedIds, readersByLongest);
    }

    /// <summary>
    /// Проверка, что возвращается пять наиболее популярных издательств за последний год
    /// </summary>
    [Fact]
    public async void GetTop5Publishers_InLastYear_ReturnsExpectedList()
    {
        var today = new DateOnly(2025, 10, 31);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("b0000000-0000-0000-0000-000000000001"),
            Guid.Parse("b0000000-0000-0000-0000-000000000002"),
            Guid.Parse("b0000000-0000-0000-0000-000000000008"),
            Guid.Parse("b0000000-0000-0000-0000-000000000003"),
            Guid.Parse("b0000000-0000-0000-0000-000000000006")
        };

        var topPublishers = await _service.GetTop5Publishers_InLastYear_ReturnsExpectedList(today);

        Assert.Equal(expectedIds, topPublishers);
    }

    /// <summary>
    /// Проверка, что возвращается пять наименее популярных книг за последний год
    /// </summary>
    [Fact]
    public async void GetBottom5Books_InLastYear_ReturnsExpectedBooks()
    {
        var today = new DateOnly(2025, 10, 14);

        var expectedIds = new List<Guid>
        {
            Guid.Parse("d0000000-0000-0000-0000-000000000019"),
            Guid.Parse("d0000000-0000-0000-0000-000000000020"),
            Guid.Parse("d0000000-0000-0000-0000-000000000016"),
            Guid.Parse("d0000000-0000-0000-0000-000000000004"),
            Guid.Parse("d0000000-0000-0000-0000-000000000017")
        };

        var bottomBooks = await _service.GetBottom5Books_InLastYear_ReturnsExpectedBooks(today);

        Assert.Equal(expectedIds, bottomBooks);
    }
}