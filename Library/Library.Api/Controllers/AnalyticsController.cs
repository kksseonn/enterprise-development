using Library.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для аналитики
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Получить все активные выданные книги
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список идентификаторов книг (Guid)</returns>
    [HttpGet("BorrowedBooks")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetBorrowedBooks(CancellationToken cancellationToken)
    {
        const string methodName = nameof(GetBorrowedBooks);
        logger.LogInformation("{Method} of {Controller} was called", methodName, GetType().Name);

        try
        {
            var result = await service.GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder(cancellationToken);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить пять наименее популярных книг за последний год
    /// </summary>
    /// <param name="today">Текущая дата. Формат: YYYY-MM-DD</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список идентификаторов книг</returns>
    [HttpGet("Bottom5Books")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetBottom5Books([FromQuery] DateOnly today, CancellationToken cancellationToken)
    {
        const string methodName = nameof(GetBottom5Books);
        logger.LogInformation("{Method} of {Controller} was called. Today: {Today}", methodName, GetType().Name, today);

        try
        {
            var result = await service.GetBottom5Books_InLastYear_ReturnsExpectedBooks(today, cancellationToken);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить пять самых активных читателей за указанный период
    /// </summary>
    /// <param name="start_date">Дата начала периода. Формат: YYYY-MM-DD</param>
    /// <param name="end_date">Дата конца периода. Формат: YYYY-MM-DD</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список идентификаторов читателей (Guid)</returns>
    [HttpGet("Top5Readers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTop5Readers([FromQuery] DateOnly start_date, [FromQuery] DateOnly end_date, CancellationToken cancellationToken)
    {
        const string methodName = nameof(GetTop5Readers);
        logger.LogInformation("{Method} of {Controller} was called. Period: {StartDate} to {EndDate}", methodName, GetType().Name, start_date, end_date);

        try
        {
            var result = await service.GetTop5Readers_InPeriod_ReturnsCorrectReaders(start_date, end_date, cancellationToken);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить читателей, бравших книги на наибольший суммарный период
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список идентификаторов читателей (Guid)</returns>
    [HttpGet("ReadersByLongestBorrowDays")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetReadersByLongestBorrowDays(CancellationToken cancellationToken)
    {
        const string methodName = nameof(GetReadersByLongestBorrowDays);
        logger.LogInformation("{Method} of {Controller} was called", methodName, GetType().Name);

        try
        {
            var result = await service.GetReaders_ByLongestTotalBorrowDays_ReturnsSortedByFullName(cancellationToken);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить пять наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="today">Текущая дата. Формат: YYYY-MM-DD</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список идентификаторов издательств (Guid)</returns>
    [HttpGet("Top5Publishers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTop5Publishers([FromQuery] DateOnly today, CancellationToken cancellationToken)
    {
        const string methodName = nameof(GetTop5Publishers);
        logger.LogInformation("{Method} of {Controller} was called. Today: {Today}", methodName, GetType().Name, today);

        try
        {
            var result = await service.GetTop5Publishers_InLastYear_ReturnsExpectedList(today, cancellationToken);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}

