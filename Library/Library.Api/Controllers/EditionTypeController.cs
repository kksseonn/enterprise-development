using Library.Application.Contracts.EditionType;
using Library.Application.Contracts.Book;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с типами изданий (EditionType)
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
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetBooks(Guid id) =>
        await ExecuteWithLogging(nameof(GetBooks), async () =>
        {
            try
            {
                var result = await service.GetBooks(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
}