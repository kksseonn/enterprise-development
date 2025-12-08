using Library.Domain.Entities;
using Library.Application.Contracts;
using Library.Domain;

namespace Library.Application.Service;

public class AnalyticsService(IRepository<Borrow> borrowRepository,
                              IRepository<Book> bookRepository,
                              IRepository<Reader> readerRepository,
                              IRepository<Publisher> publisherRepository) : IAnalyticsService

{
    /// <summary>
    /// Проверка активных выдач книг по названию
    /// </summary>
    public async Task<List<Guid>> GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder(CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll(); 
        var books = await bookRepository.GetAll();   

        var bookById = books.ToDictionary(b => b.Id);

        var resultIds = borrows
            .Where(b => b.ReturnDate is null)                 
            .OrderBy(b => bookById[b.BookId].Title)          
            .Select(b => b.BookId)                            
            .ToList();

        return resultIds;
    }

    /// <summary>
    /// Проверка, что возвращается пять самых активных читателей за указанный период
    /// </summary>
    public async Task<List<Guid>> GetTop5Readers_InPeriod_ReturnsCorrectReaders(DateOnly start_date, DateOnly end_date, CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll();
        var readers = await readerRepository.GetAll();

        var readerById = readers.ToDictionary(r => r.Id);

        var topReaders = borrows
            .Where(b => b.BorrowDate >= start_date && b.BorrowDate <= end_date)
            .GroupBy(b => readerById[b.ReaderId].Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(5)
            .ToList();

        return topReaders;
    }

    /// <summary>
    /// Проверка выборки читателей, бравших книги на наибольший период времени
    /// </summary>
    public async Task<List<Guid>> GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName(CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll();
        var readers = await readerRepository.GetAll();

        var readerById = readers.ToDictionary(r => r.Id);

        var readerMaxDays = borrows
               .GroupBy(b => readerById[b.ReaderId].Id)
               .Select(g => new
               {
                   ReaderId = g.Key!,
                   MaxDays = g.Max(b => b.Days)
               })
               .ToList();

        var globalMax = readerMaxDays.Max(x => x.MaxDays);

        var readersByLongest = readerMaxDays
            .Where(x => x.MaxDays == globalMax)
            .OrderBy(x => readerById[x.ReaderId].Surname)
            .ThenBy(x => readerById[x.ReaderId].Name)
            .ThenBy(x => readerById[x.ReaderId].Patronymic)
            .Select(x => readerById[x.ReaderId].Id)
            .ToList();

        return readersByLongest;
    }

    /// <summary>
    /// Проверка, что возвращается пять наиболее популярных издательств за последний год
    /// </summary>
    public async Task<List<Guid>> GetTop5Publishers_InLastYear_ReturnsExpectedList(DateOnly today, CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll();
        var books = await bookRepository.GetAll();
        var publishers = await publisherRepository.GetAll();

        var publisherById = publishers.ToDictionary(p => p.Id);
        var bookById = books.ToDictionary(b => b.Id);

        var oneYearAgo = today.AddYears(-1);

        var topPublishers = borrows
            .Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today)
            .GroupBy(b => bookById[b.BookId].PublisherId)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(5)
            .ToList();

        return topPublishers;
    }

    /// <summary>
    /// Проверка, что возвращается пять наименее популярных книг за последний год
    /// </summary>
    public async Task<List<Guid>> GetBottom5Books_InLastYear_ReturnsExpectedBooks(DateOnly today, CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll(cancellationToken);
        var books = await bookRepository.GetAll(cancellationToken);

        var bookById = books.ToDictionary(b => b.Id);

        var oneYearAgo = today.AddYears(-1);

        var borrowsInPeriod = borrows
            .Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today)
            .ToList();

        var bottomBooks = bookById.Values
            .GroupJoin(
                borrowsInPeriod,
                book => book.Id,
                borrow => borrow.BookId,
                (book, borrowGroup) => new
                {
                    Book = book,
                    BorrowCount = borrowGroup.Count()
                })
            .OrderBy(x => x.BorrowCount)
            .ThenBy(x => x.Book.Title)
            .Select(x => x.Book.Id)
            .Take(5)
            .ToList();

        return bottomBooks;
    }

}