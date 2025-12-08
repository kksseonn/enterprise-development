using Library.Application.Contracts.Book;
using Library.Application.Contracts.Borrow;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с книгами (Book)
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
    public async Task<ActionResult<IReadOnlyList<BorrowDto>>> GetBorrows(Guid id) =>
        await ExecuteWithLogging(nameof(GetBorrows), async () =>
        {
            try
            {
                var result = await service.GetBorrows(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
}