using Library.Application.Contracts.Book;
using Library.Application.Contracts.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с издательствами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PublisherController(
    IPublisherCrudService service,
    ILogger<PublisherController> logger)
    : CrudControllerBase<PublisherDto, PublisherCrudDto, Guid>(service, logger)
{
    /// <summary>
    /// Получить все книги для конкретного издательства
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <returns>Список DTO книг издательства</returns>
    [HttpGet("{id}/Books")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetBooks(Guid id)
    {
        const string methodName = nameof(GetBooks);
        logger.LogInformation("{Method} of {Controller} was called for Publisher ID: {Id}", methodName, GetType().Name, id);

        try
        {
            var result = await service.GetBooks(id);
            logger.LogInformation("{Method} of {Controller} executed successfully. Books found: {Count}", methodName, GetType().Name, result.Count);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("{Method} of {Controller}: Publisher with ID {Id} not found.", methodName, GetType().Name, id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller} for Publisher ID: {Id}", methodName, GetType().Name, id);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}