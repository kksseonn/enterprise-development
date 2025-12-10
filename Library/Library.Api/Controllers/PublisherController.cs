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