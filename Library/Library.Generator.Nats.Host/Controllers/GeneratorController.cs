using Library.Generator.Nats.Host.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Generator.Nats.Host.Controllers;

[ApiController]
[Route("api/generator")]
public class GeneratorController : ControllerBase
{
    private readonly IBorrowsGenerator _generator;

    public GeneratorController(IBorrowsGenerator generator)
    {
        _generator = generator;
    }

    /// <summary>
    /// Генерация тестовых карточек Borrow и отправка их батчами в NATS JetStream
    /// </summary>
    /// <param name="batchSize">Количество элементов в одном батче</param>
    /// <param name="batchesCount">Количество батчей</param>
    [HttpPost("borrows")]
    public async Task<IActionResult> GenerateBorrows(
        [FromQuery] int batchSize = 10,
        [FromQuery] int batchesCount = 1,
        CancellationToken cancellationToken = default)
    {
        if (batchSize <= 0 || batchesCount <= 0)
            return BadRequest("batchSize и batchesCount должны быть больше 0");

        await _generator.GenerateAsync(
            batchSize,
            batchesCount,
            cancellationToken);

        return Ok(new
        {
            Message = "Генерация и отправка данных завершена",
            BatchSize = batchSize,
            BatchesCount = batchesCount
        });
    }
}