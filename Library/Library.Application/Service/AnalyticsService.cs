using Library.Application.Contracts;
using Library.Domain;
using Library.Domain.Entities;

namespace Library.Application.Service;

/// <summary>
/// Сервис аналитики для работы с выдачами книг, читателями и издательствами
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IRepository<Borrow> _borrowRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Reader> _readerRepository;
    private readonly IRepository<Publisher> _publisherRepository;

    /// <summary>
    /// Конструктор сервиса аналитики
    /// </summary>
    public AnalyticsService(
        IRepository<Borrow> borrowRepository,
        IRepository<Book> bookRepository,
        IRepository<Reader> readerRepository,
        IRepository<Publisher> publisherRepository)
    {
        _borrowRepository = borrowRepository;
        _bookRepository = bookRepository;
        _readerRepository = readerRepository;
        _publisherRepository = publisherRepository;
    }

    /// <summary>
    /// Возвращает активные выдачи книг, отсортированные по названию
    /// </summary>
    public async Task<List<Guid>> GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder(CancellationToken cancellationToken = default)
    {
        var borrows = await _borrowRepository.GetAll(cancellationToken);
        var books = await _bookRepository.GetAll(cancellationToken);

        var bookById = books.ToDictionary(b => b.Id);

        return borrows
            .Where(b => b.ReturnDate is null)
            .OrderBy(b => bookById[b.BookId].Title)
            .Select(b => b.BookId)
            .ToList();
    }

    /// <summary>
    /// Возвращает пять самых активных читателей за указанный период
    /// </summary>
    public async Task<List<Guid>> GetTop5Readers_InPeriod_ReturnsCorrectReaders(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        var borrows = await _borrowRepository.GetAll(cancellationToken);
        var readers = await _readerRepository.GetAll(cancellationToken);

        var readerById = readers.ToDictionary(r => r.Id);

        return borrows
            .Where(b => b.BorrowDate >= startDate && b.BorrowDate <= endDate)
            .GroupBy(b => readerById[b.ReaderId].Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Возвращает читателей, браших книги на наибольший период времени, отсортированных по ФИО
    /// </summary>
    public async Task<List<Guid>> GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName(CancellationToken cancellationToken = default)
    {
        var borrows = await _borrowRepository.GetAll(cancellationToken);
        var readers = await _readerRepository.GetAll(cancellationToken);

        var readerById = readers.ToDictionary(r => r.Id);

        var readerMaxDays = borrows
            .GroupBy(b => readerById[b.ReaderId].Id)
            .Select(g => new
            {
                ReaderId = g.Key,
                MaxDays = g.Max(b => b.Days)
            })
            .ToList();

        var globalMax = readerMaxDays.Max(x => x.MaxDays);

        return readerMaxDays
            .Where(x => x.MaxDays == globalMax)
            .OrderBy(x => readerById[x.ReaderId].Surname)
            .ThenBy(x => readerById[x.ReaderId].Name)
            .ThenBy(x => readerById[x.ReaderId].Patronymic)
            .Select(x => readerById[x.ReaderId].Id)
            .ToList();
    }

    /// <summary>
    /// Возвращает пять наиболее популярных издательств за последний год
    /// </summary>
    public async Task<List<Guid>> GetTop5Publishers_InLastYear_ReturnsExpectedList(DateOnly today, CancellationToken cancellationToken = default)
    {
        var borrows = await _borrowRepository.GetAll(cancellationToken);
        var books = await _bookRepository.GetAll(cancellationToken);
        var publishers = await _publisherRepository.GetAll(cancellationToken);

        var publisherById = publishers.ToDictionary(p => p.Id);
        var bookById = books.ToDictionary(b => b.Id);

        var oneYearAgo = today.AddYears(-1);

        return borrows
            .Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today)
            .GroupBy(b => bookById[b.BookId].PublisherId)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Возвращает пять наименее популярных книг за последний год
    /// </summary>
    public async Task<List<Guid>> GetBottom5Books_InLastYear_ReturnsExpectedBooks(DateOnly today, CancellationToken cancellationToken = default)
    {
        var borrows = await _borrowRepository.GetAll(cancellationToken);
        var books = await _bookRepository.GetAll(cancellationToken);

        var bookById = books.ToDictionary(b => b.Id);

        var oneYearAgo = today.AddYears(-1);

        var borrowsInPeriod = borrows
            .Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today)
            .ToList();

        return bookById.Values
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
    }
}