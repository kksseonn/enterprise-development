using Library.Application.Contracts;
using Library.Application.Contracts.Borrow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с выдачами книг (Borrow)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BorrowController : ControllerBase
{
    private readonly IApplicationCrudService<BorrowDto, BorrowDto, Guid> _crudService;
    private readonly ILogger<BorrowController> _logger;

    /// <summary>
    /// Конструктор контроллера Borrow
    /// </summary>
    /// <param name="crudService">Сервис CRUD для Borrow</param>
    /// <param name="logger">Логгер контроллера</param>
    public BorrowController(
        IApplicationCrudService<BorrowDto, BorrowDto, Guid> crudService,
        ILogger<BorrowController> logger)
    {
        _crudService = crudService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список всех выдач книг
    /// </summary>
    /// <returns>Список DTO Borrow</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<BorrowDto>>> GetAll()
    {
        return await ExecuteWithLogging(nameof(GetAll), async () =>
        {
            var result = await _crudService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// Получить выдачу книги по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <returns>DTO Borrow</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<BorrowDto>> Get(Guid id)
    {
        return await ExecuteWithLogging(nameof(Get), async () =>
        {
            try
            {
                var result = await _crudService.Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }

    /// <summary>
    /// Создать новую выдачу книги
    /// </summary>
    /// <param name="dto">DTO создаваемой выдачи</param>
    /// <returns>Созданный DTO Borrow</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<BorrowDto>> Create([FromBody] BorrowDto dto)
    {
        return await ExecuteWithLogging(nameof(Create), async () =>
        {
            var created = await _crudService.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        });
    }

    /// <summary>
    /// Обновить существующую выдачу книги
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <param name="dto">DTO с обновлёнными данными</param>
    /// <returns>Обновлённый DTO Borrow</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<BorrowDto>> Update(Guid id, [FromBody] BorrowDto dto)
    {
        return await ExecuteWithLogging(nameof(Update), async () =>
        {
            try
            {
                var updated = await _crudService.Update(dto, id);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }

    /// <summary>
    /// Удалить выдачу книги по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <returns>Статус выполнения операции</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(Guid id)
    {
        return await ExecuteWithLogging(nameof(Delete), async () =>
        {
            var deleted = await _crudService.Delete(id);
            return deleted ? NoContent() : NotFound();
        });
    }

    /// <summary>
    /// Вспомогательный метод для логирования и обработки исключений
    /// </summary>
    /// <param name="method">Имя метода контроллера</param>
    /// <param name="action">Функция действия</param>
    /// <returns>ActionResult выполнения действия</returns>
    private async Task<ActionResult> ExecuteWithLogging(string method, Func<Task<ActionResult>> action)
    {
        var controllerName = nameof(BorrowController);

        _logger.LogInformation("{Method} of {Controller} was called", method, controllerName);
        try
        {
            var result = await action();
            _logger.LogInformation("{Method} of {Controller} executed successfully", method, controllerName);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in {Method} of {Controller}", method, controllerName);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}