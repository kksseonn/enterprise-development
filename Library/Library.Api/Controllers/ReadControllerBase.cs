using Library.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public abstract class ReadControllerBase<TDto, TKey> : ControllerBase
    where TDto : class
    where TKey : struct
{
    protected readonly IApplicationReadService<TDto, TKey> _appService;
    protected readonly ILogger<ReadControllerBase<TDto, TKey>> _logger;

    protected ReadControllerBase(
        IApplicationReadService<TDto, TKey> appService,
        ILogger<ReadControllerBase<TDto, TKey>> logger)
    {
        _appService = appService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
        => await ExecuteWithLogging(nameof(GetAll), async () =>
        {
            var entities = await _appService.GetAll();
            return Ok(entities);
        });

    /// <summary>
    /// Retrieves an entity by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Get(TKey id)
        => await ExecuteWithLogging(nameof(Get), async () =>
        {
            try
            {
                var entity = await _appService.Get(id);
                return Ok(entity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

    /// <summary>
    /// Executes an action with consistent logging and error handling.
    /// </summary>
    protected async Task<ActionResult> ExecuteWithLogging(string method, Func<Task<ActionResult>> action)
    {
        _logger.LogInformation("{Method} of {Controller} was called", method, GetType().Name);

        try
        {
            var result = await action();
            _logger.LogInformation("{Method} of {Controller} executed successfully", method, GetType().Name);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in {Method} of {Controller}", method, GetType().Name);
            return StatusCode(500, new { ex.Message, InnerException = ex.InnerException?.Message });
        }
    }
}
