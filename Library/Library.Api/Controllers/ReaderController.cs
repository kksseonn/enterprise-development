using Library.Application.Contracts.Borrow;
using Library.Application.Contracts.Reader;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с читателями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReaderController(
    IReaderCrudService service,
    ILogger<ReaderController> logger)
    : CrudControllerBase<ReaderDto, ReaderCrudDto, Guid>(service, logger)
{
    /// <summary>
    /// Получить все записи о выдачах для конкретного читателя
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
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