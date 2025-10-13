using Library.Tests.Fixtures;

namespace Library.Tests;

public class LibraryDomainTest(DataFixture fixture): IClassFixture<DataFixture>
{
    private readonly DataFixture _fixture = fixture;

    [Fact]
    public void GetBorrowedBook_OrdereByBookTitle_ReturnCorrectBook()
    {
        List<Guid> expectedIds = [
            Guid.Parse("d0000000-0000-0000-0000-000000000007"),
            Guid.Parse("d0000000-0000-0000-0000-000000000008"),
            Guid.Parse("d0000000-0000-0000-0000-000000000001"),
            Guid.Parse("d0000000-0000-0000-0000-000000000002"),
            Guid.Parse("d0000000-0000-0000-0000-000000000006"),
            Guid.Parse("d0000000-0000-0000-0000-000000000005"),
            Guid.Parse("d0000000-0000-0000-0000-000000000009"),
            Guid.Parse("d0000000-0000-0000-0000-000000000010"),
            Guid.Parse("d0000000-0000-0000-0000-000000000003"),
        ];
        var activeBorrows = _fixture.Borrows.Where(b => b.ReturnDate == null);
        var resultIds = activeBorrows
            .OrderBy(b => b.Book.Title)
            .Select(b => b.Book.Id)
            .ToList();

        Assert.Equal(expectedIds, resultIds);
    }

    [Fact]
    public void GetTop5Readers_InPeriod_ReturnCorrectReaders()
    {
        var startDate = new DateOnly(2024, 10, 31);
        var endDate = new DateOnly(2025, 10, 31);

        List<Guid> expectedIds = new()
        {
            Guid.Parse("c0000000-0000-0000-0000-000000000001"),
            Guid.Parse("c0000000-0000-0000-0000-000000000002"),
            Guid.Parse("c0000000-0000-0000-0000-000000000003"),
            Guid.Parse("c0000000-0000-0000-0000-000000000004"),
            Guid.Parse("c0000000-0000-0000-0000-000000000010")
        };

        var topReaders = _fixture.Borrows
            .Where(b => b.BorrowDate >= startDate && b.BorrowDate <= endDate)
            .GroupBy(b => b.Reader.Id)
            .Select(g => new { ReaderId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.ReaderId)
            .Take(5)
            .Select(x => x.ReaderId)
            .ToList();

        Assert.Equal(expectedIds, topReaders);
    }
}