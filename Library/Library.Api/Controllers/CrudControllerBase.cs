using Library.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Базовый контроллер CRUD операций
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления</typeparam>
/// <typeparam name="TKey">Тип ключа</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationCrudService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger)
    : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создает новый объект
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto newDto)
        => await ExecuteWithLogging(nameof(Create), async () =>
        {
            try
            {
                var created = await appService.Create(newDto);
                return CreatedAtAction(nameof(Create), created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        });

    /// <summary>
    /// Обновляет объект по идентификатору
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<TDto>> Update(TKey id, [FromBody] TCreateUpdateDto newDto)
        => await ExecuteWithLogging(nameof(Update), async () =>
        {
            try
            {
                var updated = await appService.Update(newDto, id);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

    /// <summary>
    /// Удаляет объект по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(TKey id)
        => await ExecuteWithLogging(nameof(Delete), async () =>
        {
            var deleted = await appService.Delete(id);
            return deleted ? Ok() : NoContent();
        });

    /// <summary>
    /// Возвращает все объекты
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
        => await ExecuteWithLogging(nameof(GetAll), async () =>
            Ok(await appService.GetAll()));

    /// <summary>
    /// Возвращает объект по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
        => await ExecuteWithLogging(nameof(Get), async () =>
        {
            try
            {
                var result = await appService.Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

    /// <summary>
    /// Выполняет действие с логированием и обработкой ошибок
    /// Версия для методов, возвращающих DTO
    /// </summary>
    protected async Task<ActionResult<TResult>> ExecuteWithLogging<TResult>(
        string method,
        Func<Task<ActionResult<TResult>>> action)
    {
        logger.LogInformation("{Method} of {Controller} was called", method, GetType().Name);

        try
        {
            var result = await action();
            logger.LogInformation("{Method} of {Controller} executed successfully", method, GetType().Name);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", method, GetType().Name);

            var errorResult = new ObjectResult($"{ex.Message}\n{ex.InnerException?.Message}") { StatusCode = 500 };
            return new ActionResult<TResult>(errorResult);
        }
    }

    /// <summary>
    /// Выполняет действие с логированием и обработкой ошибок
    /// Версия для методов, возвращающих только ActionResult
    /// </summary>
    protected async Task<ActionResult> ExecuteWithLogging(
        string method,
        Func<Task<ActionResult>> action)
    {
        logger.LogInformation("{Method} of {Controller} was called", method, GetType().Name);

        try
        {
            var result = await action();
            logger.LogInformation("{Method} of {Controller} executed successfully", method, GetType().Name);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", method, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}