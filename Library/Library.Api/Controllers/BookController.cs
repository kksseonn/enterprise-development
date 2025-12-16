using Library.Application.Contracts.Book;
using Library.Application.Contracts.Borrow;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с книгами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookController(
    IBookCrudService service,
    ILogger<BookController> logger)
    : CrudControllerBase<BookDto, BookCrudDto, Guid>(service, logger)
{
    /// <summary>
    /// Получить все записи о выдачах для конкретной книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>Список DTO записей о выдачах</returns>
    [HttpGet("{id}/Borrows")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<BorrowDto>>> GetBorrows(Guid id)
    {
        const string methodName = nameof(GetBorrows);
        logger.LogInformation("{Method} of {Controller} was called for Book ID: {Id}", methodName, GetType().Name, id);

        try
        {
            var result = await service.GetBorrows(id);
            logger.LogInformation("{Method} of {Controller} executed successfully. Borrows found: {Count}", methodName, GetType().Name, result.Count);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("{Method} of {Controller}: Book with ID {Id} not found.", methodName, GetType().Name, id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller} for Book ID: {Id}", methodName, GetType().Name, id);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}