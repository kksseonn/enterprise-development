using Library.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ReadControllerBase<TDto, TKey>(
    IApplicationReadService<TDto, TKey> appService,
    ILogger<ReadControllerBase<TDto, TKey>> logger)
    : ControllerBase
    where TDto : class
    where TKey : struct
{
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
        => await ExecuteWithLogging(nameof(GetAll), async () => Ok(await appService.GetAll()));

    [HttpGet("{id}")]
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

    protected async Task<ActionResult> ExecuteWithLogging(string method, Func<Task<ActionResult>> action)
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
