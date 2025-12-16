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
    /// Возвращает все объекты
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll()
    {
        const string methodName = nameof(GetAll);
        logger.LogInformation("{Method} of {Controller} was called", methodName, GetType().Name);

        try
        {
            var result = await appService.GetAll();
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает объект по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        const string methodName = nameof(Get);
        logger.LogInformation("{Method} of {Controller} was called with ID: {Id}", methodName, GetType().Name, id);

        try
        {
            var result = await appService.Get(id);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("{Method} of {Controller}: Item with ID {Id} not found", methodName, GetType().Name, id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Создает новый объект
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto newDto)
    {
        const string methodName = nameof(Create);
        logger.LogInformation("{Method} of {Controller} was called", methodName, GetType().Name);

        try
        {
            var created = await appService.Create(newDto);
            logger.LogInformation("{Method} of {Controller} executed successfully. Created ID: {Id}", methodName, GetType().Name, created.GetType().GetProperty("Id")?.GetValue(created));

            return CreatedAtAction(nameof(Get), new { id = created.GetType().GetProperty("Id")?.GetValue(created) }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Обновляет объект по идентификатору
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Update(TKey id, [FromBody] TCreateUpdateDto newDto)
    {
        const string methodName = nameof(Update);
        logger.LogInformation("{Method} of {Controller} was called with ID: {Id}", methodName, GetType().Name, id);

        try
        {
            var updated = await appService.Update(newDto, id);
            logger.LogInformation("{Method} of {Controller} executed successfully", methodName, GetType().Name);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            logger.LogWarning("{Method} of {Controller}: Item with ID {Id} not found for update", methodName, GetType().Name, id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Удаляет объект по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(TKey id)
    {
        const string methodName = nameof(Delete);
        logger.LogInformation("{Method} of {Controller} was called with ID: {Id}", methodName, GetType().Name, id);

        try
        {
            var deleted = await appService.Delete(id);

            if (deleted)
            {
                logger.LogInformation("{Method} of {Controller} executed successfully. Item deleted.", methodName, GetType().Name);
                return NoContent();
            }
            else
            {
                logger.LogWarning("{Method} of {Controller}: Item with ID {Id} not found for deletion", methodName, GetType().Name, id);
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in {Method} of {Controller}", methodName, GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}