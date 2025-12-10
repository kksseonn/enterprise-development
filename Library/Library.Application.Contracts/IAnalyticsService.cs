namespace Library.Application.Contracts;

/// <summary>
/// Интерфейс для аналитических сервисов библиотеки
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает все активные выданные книги, отсортированные по названию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов книг</returns>
    public Task<List<Guid>> GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает пять наименее популярных книг за последний год
    /// </summary>
    /// <param name="today">Текущая дата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов книг</returns>
    public Task<List<Guid>> GetBottom5Books_InLastYear_ReturnsExpectedBooks(DateOnly today, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает пять самых активных читателей за указанный период
    /// </summary>
    /// <param name="start_date">Дата начала периода</param>
    /// <param name="end_date">Дата конца периода</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов читателей</returns>
    public Task<List<Guid>> GetTop5Readers_InPeriod_ReturnsCorrectReaders(DateOnly start_date, DateOnly end_date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает читателей, бравших книги на наибольший суммарный период, отсортированных по ФИО
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов читателей</returns>
    public Task<List<Guid>> GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает пять наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="today">Текущая дата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список идентификаторов издательств</returns>
    public Task<List<Guid>> GetTop5Publishers_InLastYear_ReturnsExpectedList(DateOnly today, CancellationToken cancellationToken = default);
}