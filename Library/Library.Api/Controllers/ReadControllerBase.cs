using Library.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ReadControllerBase<TDto, TKey> : ControllerBase
    where TDto : class
    where TKey : struct
{
    protected readonly IApplicationReadService<TDto, TKey> _appService;
    protected readonly ILogger<ReadControllerBase<TDto, TKey>> _logger;

    protected ReadControllerBase(IApplicationReadService<TDto, TKey> appService,
                                 ILogger<ReadControllerBase<TDto, TKey>> logger)
    {
        _appService = appService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll() =>
        await ExecuteWithLogging(nameof(GetAll), async () => Ok(await _appService.GetAll()));

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id) =>
        await ExecuteWithLogging(nameof(Get), async () =>
        {
            try
            {
                var result = await _appService.Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

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
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}
