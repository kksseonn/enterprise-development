using Library.Application.Contracts.EditionType;
using Library.Application.Contracts.Book;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с типами изданий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EditionTypeController(
    IEditionTypeCrudService service,
    ILogger<EditionTypeController> logger)
    : CrudControllerBase<EditionTypeDto, EditionTypeCrudDto, Guid>(service, logger)
{
    /// <summary>
    /// Получить все книги для конкретного типа издания
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <returns>Список DTO книг этого типа издания</returns>
    [HttpGet("{id}/Books")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetBooks(Guid id)
    {
        const string methodName = nameof(GetBooks);
        logger.LogInformation("{Method} of {Controller} was called for EditionType ID: {Id}", methodName, GetType().Name, id);

        try
        {
            var result = await service.GetBooks(id);
            logger.LogInformation("{Method} of {Controller} executed successfully. Books found: {Count}", methodName, GetType().Name, result.Count);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("{Method} of {Controller}: EditionType with ID {Id} not found.", methodName, GetType().Name, id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller} for EditionType ID: {Id}", methodName, GetType().Name, id);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}