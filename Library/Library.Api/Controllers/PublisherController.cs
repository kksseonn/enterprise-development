using Library.Application.Contracts;
using Library.Application.Contracts.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с издательствами (Publisher)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PublisherController : ControllerBase
{
    private readonly IApplicationCrudService<PublisherDto, PublisherDto, Guid> _crudService;
    private readonly ILogger<PublisherController> _logger;

    /// <summary>
    /// Конструктор контроллера Publisher
    /// </summary>
    /// <param name="crudService">Сервис CRUD для издательств</param>
    /// <param name="logger">Логгер контроллера</param>
    public PublisherController(
        IApplicationCrudService<PublisherDto, PublisherDto, Guid> crudService,
        ILogger<PublisherController> logger)
    {
        _crudService = crudService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список всех издательств
    /// </summary>
    /// <returns>Список DTO издательств</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<PublisherDto>>> GetAll()
    {
        return await ExecuteWithLogging(nameof(GetAll), async () =>
        {
            var result = await _crudService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// Получить издательство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <returns>DTO издательства</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PublisherDto>> Get(Guid id)
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
    /// Создать новое издательство
    /// </summary>
    /// <param name="dto">DTO создаваемого издательства</param>
    /// <returns>Созданный DTO издательства</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<PublisherDto>> Create([FromBody] PublisherDto dto)
    {
        return await ExecuteWithLogging(nameof(Create), async () =>
        {
            var created = await _crudService.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        });
    }

    /// <summary>
    /// Обновить существующее издательство
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <param name="dto">DTO с обновлёнными данными</param>
    /// <returns>Обновлённый DTO издателя</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<PublisherDto>> Update(Guid id, [FromBody] PublisherDto dto)
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
    /// Удалить издательство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
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
        _logger.LogInformation("{Method} of {Controller} was called", method, nameof(PublisherController));
        try
        {
            var result = await action();
            _logger.LogInformation("{Method} of {Controller} executed successfully", method, nameof(PublisherController));
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in {Method} of {Controller}", method, nameof(PublisherController));
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}