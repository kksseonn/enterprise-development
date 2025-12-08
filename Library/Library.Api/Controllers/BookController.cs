using Library.Application.Contracts;
using Library.Application.Contracts.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с книгами (Book)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IApplicationCrudService<BookDto, BookDto, Guid> _crudService;
    private readonly ILogger<BookController> _logger;

    /// <summary>
    /// Конструктор контроллера Book
    /// </summary>
    /// <param name="crudService">Сервис CRUD для Book</param>
    /// <param name="logger">Логгер контроллера</param>
    public BookController(
        IApplicationCrudService<BookDto, BookDto, Guid> crudService,
        ILogger<BookController> logger)
    {
        _crudService = crudService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список всех книг
    /// </summary>
    /// <returns>Список DTO книг</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetAll()
    {
        return await ExecuteWithLogging(nameof(GetAll), async () =>
        {
            var result = await _crudService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// Получить книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>DTO книги</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<BookDto>> Get(Guid id)
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
    /// Создать новую книгу
    /// </summary>
    /// <param name="dto">DTO создаваемой книги</param>
    /// <returns>Созданный DTO книги</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<BookDto>> Create([FromBody] BookDto dto)
    {
        return await ExecuteWithLogging(nameof(Create), async () =>
        {
            var created = await _crudService.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        });
    }

    /// <summary>
    /// Обновить существующую книгу
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <param name="dto">DTO с обновлёнными данными</param>
    /// <returns>Обновлённый DTO книги</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<BookDto>> Update(Guid id, [FromBody] BookDto dto)
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
    /// Удалить книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
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
    /// Вспомогательный метод для логирования вызовов и обработки исключений
    /// </summary>
    /// <param name="method">Имя метода контроллера</param>
    /// <param name="action">Функция действия</param>
    /// <returns>ActionResult выполнения действия</returns>
    private async Task<ActionResult> ExecuteWithLogging(string method, Func<Task<ActionResult>> action)
    {
        var controllerName = nameof(BookController);

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