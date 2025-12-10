using Library.Application.Contracts;
using Library.Domain;
using Library.Domain.Entities;
using Library.Infrastructure.Repository;
using System.Linq;

namespace Library.Application.Service;

/// <summary>
/// Сервис аналитики для работы с выдачами книг, читателями и издательствами
/// </summary>
public class AnalyticsService(
    IRepository<Borrow> borrowRepository,
    IRepository<Book> bookRepository,
    IRepository<Reader> readerRepository) : IAnalyticsService
{
    /// <summary>
    /// Получает все активные выданные книги, отсортированные по названию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов книг</returns>
    public async Task<List<Guid>> GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder(CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll(cancellationToken);
        var books = await bookRepository.GetAll(cancellationToken);

        var bookById = books.ToDictionary(b => b.Id);

        return borrows
            .Where(b => b.ReturnDate is null)
            .OrderBy(b => bookById.TryGetValue(b.BookId, out var book) ? book.Title : string.Empty)
            .Select(b => b.BookId)
            .ToList();
    }

    /// <summary>
    /// Получает пять наименее популярных книг за последний год
    /// </summary>
    /// <param name="today">Текущая дата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов книг</returns>
    public async Task<List<Guid>> GetBottom5Books_InLastYear_ReturnsExpectedBooks(DateOnly today, CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll(cancellationToken);
        var books = await bookRepository.GetAll(cancellationToken);

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

    /// <summary>
    /// Получает пять самых активных читателей за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата конца периода</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов читателей</returns>
    public async Task<List<Guid>> GetTop5Readers_InPeriod_ReturnsCorrectReaders(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll(cancellationToken);

        return borrows
            .Where(b => b.BorrowDate >= startDate && b.BorrowDate <= endDate)
            .GroupBy(b => b.ReaderId)
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
        var borrows = await borrowRepository.GetAll(cancellationToken);
        var readers = await readerRepository.GetAll(cancellationToken);

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
    /// Получает пять наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="today">Текущая дата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов издательств</returns>
    public async Task<List<Guid>> GetTop5Publishers_InLastYear_ReturnsExpectedList(DateOnly today, CancellationToken cancellationToken = default)
    {
        var borrows = await borrowRepository.GetAll(cancellationToken);
        var books = await bookRepository.GetAll(cancellationToken);

        var bookById = books.ToDictionary(b => b.Id);

        var oneYearAgo = today.AddYears(-1);

        return borrows
            .Where(b => b.BorrowDate >= oneYearAgo && b.BorrowDate <= today)
            .GroupBy(b => bookById.TryGetValue(b.BookId, out var book) ? book.PublisherId : Guid.Empty)
            .Where(g => g.Key != Guid.Empty)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(5)
            .ToList();
    }
}