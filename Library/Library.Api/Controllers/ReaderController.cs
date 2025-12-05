using Library.Application.Contracts;
using Library.Application.Contracts.Reader;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReaderController : ControllerBase
{
    private readonly IApplicationCrudService<ReaderDto, ReaderDto, Guid> _crudService;
    private readonly ILogger<ReaderController> _logger;

    public ReaderController(
        IApplicationCrudService<ReaderDto, ReaderDto, Guid> crudService,
        ILogger<ReaderController> logger)
    {
        _crudService = crudService;
        _logger = logger;
    }


    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<ReaderDto>>> GetAll()
    {
        return await ExecuteWithLogging(nameof(GetAll), async () =>
        {
            var result = await _crudService.GetAll();
            return Ok(result);
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ReaderDto>> Get(Guid id)
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


    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ReaderDto>> Create([FromBody] ReaderDto dto)
    {
        return await ExecuteWithLogging(nameof(Create), async () =>
        {
            var created = await _crudService.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        });
    }


    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ReaderDto>> Update(Guid id, [FromBody] ReaderDto dto)
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


    private async Task<ActionResult> ExecuteWithLogging(string method, Func<Task<ActionResult>> action)
    {
        _logger.LogInformation("{Method} of {Controller} was called", method, nameof(ReaderController));
        try
        {
            var result = await action();
            _logger.LogInformation("{Method} of {Controller} executed successfully", method, nameof(ReaderController));
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in {Method} of {Controller}", method, nameof(ReaderController));
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}
