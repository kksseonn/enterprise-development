using Library.Tests.Fixtures;

namespace Library.Tests;

public class LibraryDomainTest(DataFixture fixture): IClassFixture<DataFixture>
{
    private readonly DataFixture _fixture = fixture;

    [Fact]
    public void GetBorrowedBook_ReturnOrderedByBookTitle()
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
    
}