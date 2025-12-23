using Library.Generator.Nats.Host.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Generator.Nats.Host.Controllers;

/// <summary>
/// Контроллер для генерации тестовых карточек Borrow и отправки их в NATS JetStream
/// </summary>
[Route("api/[controller]")]
[ApiController]
public sealed class GeneratorController(IBorrowsGenerator generator) : ControllerBase
{

    /// <summary>
    /// Генерация тестовых карточек Borrow и отправка их батчами в NATS JetStream
    /// </summary>
    /// <param name="batchSize">Количество элементов в одном батче</param>
    /// <param name="batchesCount">Количество батчей</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения операции</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GenerateBorrows(
        [FromQuery] int batchSize = 10,
        [FromQuery] int batchesCount = 1,
        CancellationToken cancellationToken = default)
    {
        if (batchSize <= 0 || batchesCount <= 0)
        {
            return BadRequest("batchSize и batchesCount должны быть больше 0");
        }

        await generator.GenerateAsync(batchSize, batchesCount, cancellationToken);

        return Ok(new
        {
            Message = "Генерация и отправка данных завершена",
            BatchSize = batchSize,
            BatchesCount = batchesCount
        });
    }
}