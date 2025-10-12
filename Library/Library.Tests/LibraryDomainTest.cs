using Library.Tests.Fixtures;

namespace Library.Tests;

public class LibraryDomainTest(DataFixture fixture): IClassFixture<DataFixture>
{
    private readonly DataFixture _fixture = fixture;
    [Fact]
    public void GetBorrowedBook_ReturnOrderedByBookTitle()
    {
        List<Guid> expectedIds = [
            Guid.Parse("d0000000-0000-0000-0000-000000000004"),
            Guid.Parse("d0000000-0000-0000-0000-000000000001"),
            Guid.Parse("d0000000-0000-0000-0000-000000000010"),
            Guid.Parse("d0000000-0000-0000-0000-000000000008"),
            Guid.Parse("d0000000-0000-0000-0000-000000000006"),
            Guid.Parse("d0000000-0000-0000-0000-000000000003"),
            Guid.Parse("d0000000-0000-0000-0000-000000000007"),
            Guid.Parse("d0000000-0000-0000-0000-000000000005"),
            Guid.Parse("d0000000-0000-0000-0000-000000000002"),
            Guid.Parse("d0000000-0000-0000-0000-000000000009"),
        ];
        
        var resultIds = _fixture.Borrows
            .OrderBy(borrow => borrow.Book.Title)
            .Select(borrow => borrow.Book.Id)
            .ToList();

        Assert.Equal(expectedIds, resultIds);
    }
    
}