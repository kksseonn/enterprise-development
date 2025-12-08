using Library.Application.Contracts;
using Library.Application.Contracts.EditionType;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с типами изданий (EditionType)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EditionTypeController : ControllerBase
{
    private readonly IApplicationCrudService<EditionTypeDto, EditionTypeDto, Guid> _crudService;
    private readonly ILogger<EditionTypeController> _logger;

    /// <summary>
    /// Конструктор контроллера EditionType
    /// </summary>
    /// <param name="crudService">Сервис CRUD для EditionType</param>
    /// <param name="logger">Логгер контроллера</param>
    public EditionTypeController(
        IApplicationCrudService<EditionTypeDto, EditionTypeDto, Guid> crudService,
        ILogger<EditionTypeController> logger)
    {
        _crudService = crudService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список всех типов изданий
    /// </summary>
    /// <returns>Список DTO EditionType</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<EditionTypeDto>>> GetAll()
    {
        return await ExecuteWithLogging(nameof(GetAll), async () =>
        {
            var result = await _crudService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// Получить тип издания по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <returns>DTO EditionType</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EditionTypeDto>> Get(Guid id)
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
    /// Создать новый тип издания
    /// </summary>
    /// <param name="dto">DTO создаваемого типа издания</param>
    /// <returns>Созданный DTO EditionType</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<EditionTypeDto>> Create([FromBody] EditionTypeDto dto)
    {
        return await ExecuteWithLogging(nameof(Create), async () =>
        {
            var created = await _crudService.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        });
    }

    /// <summary>
    /// Обновить существующий тип издания
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <param name="dto">DTO с обновлёнными данными</param>
    /// <returns>Обновлённый DTO EditionType</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<EditionTypeDto>> Update(Guid id, [FromBody] EditionTypeDto dto)
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
    /// Удалить тип издания по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <returns>Статус выполнения операции</returns>
    [HttpDelete("{id}")]
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
        _logger.LogInformation("{Method} of {Controller} was called", method, nameof(EditionTypeController));
        try
        {
            var result = await action();
            _logger.LogInformation("{Method} of {Controller} executed successfully", method, nameof(EditionTypeController));
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in {Method} of {Controller}", method, nameof(EditionTypeController));
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}