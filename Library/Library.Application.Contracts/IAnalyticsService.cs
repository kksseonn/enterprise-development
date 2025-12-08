namespace Library.Application.Contracts;


public interface IAnalyticsService
{
    /// <summary>
    /// Проверка активных выдач книг по названию
    /// </summary>
    public Task<List<Guid>> GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder(CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверка, что возвращается пять самых активных читателей за указанный период
    /// </summary>
    public Task<List<Guid>> GetTop5Readers_InPeriod_ReturnsCorrectReaders(DateOnly start_date, DateOnly end_date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверка выборки читателей, бравших книги на наибольший период времени
    /// </summary>
    public Task<List<Guid>> GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName(CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверка, что возвращается пять наиболее популярных издательств за последний год
    /// </summary>
    public Task<List<Guid>> GetTop5Publishers_InLastYear_ReturnsExpectedList(DateOnly today, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверка, что возвращается пять наименее популярных книг за последний год
    /// </summary>
    public Task<List<Guid>> GetBottom5Books_InLastYear_ReturnsExpectedBooks(DateOnly today, CancellationToken cancellationToken = default);
}
