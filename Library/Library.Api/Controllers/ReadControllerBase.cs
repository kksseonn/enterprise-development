using Library.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Базовый контроллер для операций чтения (Read) над сущностями.
/// Предоставляет стандартные методы GET для всех и по идентификатору.
/// </summary>
/// <typeparam name="TDto">Тип DTO сущности</typeparam>
/// <typeparam name="TKey">Тип ключа сущности</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class ReadControllerBase<TDto, TKey> : ControllerBase
    where TDto : class
    where TKey : struct
{
    /// <summary>
    /// Сервис чтения для работы с сущностями
    /// </summary>
    protected readonly IApplicationReadService<TDto, TKey> _appService;

    /// <summary>
    /// Логгер контроллера
    /// </summary>
    protected readonly ILogger<ReadControllerBase<TDto, TKey>> _logger;

    /// <summary>
    /// Конструктор базового контроллера чтения
    /// </summary>
    /// <param name="appService">Сервис чтения</param>
    /// <param name="logger">Логгер</param>
    protected ReadControllerBase(IApplicationReadService<TDto, TKey> appService,
                                 ILogger<ReadControllerBase<TDto, TKey>> logger)
    {
        _appService = appService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список всех сущностей
    /// </summary>
    /// <returns>Список DTO всех сущностей</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll() =>
        await ExecuteWithLogging(nameof(GetAll), async () => Ok(await _appService.GetAll()));

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>DTO сущности с указанным идентификатором</returns>
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

    /// <summary>
    /// Вспомогательный метод для логирования вызова метода и обработки исключений
    /// </summary>
    /// <param name="method">Имя метода контроллера</param>
    /// <param name="action">Функция действия, выполняемая внутри метода</param>
    /// <returns>ActionResult выполнения действия</returns>
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