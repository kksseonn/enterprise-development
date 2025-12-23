using Bogus;
using Library.Application.Contracts.Borrow;

namespace Library.Generator.Nats.Host.Services;

/// <summary>
/// Генератор тестовых карточек Borrow и отправка их в NATS
/// </summary>
/// <param name="producer">Продюсер для публикации данных в NATS</param>
/// <param name="logger">Интерфейс логирования</param>
public sealed class BorrowsGenerator(
    BorrowNatsProducer producer,
    ILogger<BorrowsGenerator> logger)
    : IBorrowsGenerator
{
    private static readonly Guid[] _validBookIds =
    {
        Guid.Parse("d0000000-0000-0000-0000-000000000001"),
        Guid.Parse("d0000000-0000-0000-0000-000000000002"),
        Guid.Parse("d0000000-0000-0000-0000-000000000003"),
        Guid.Parse("d0000000-0000-0000-0000-000000000004"),
        Guid.Parse("d0000000-0000-0000-0000-000000000005")
    };

    private static readonly Guid[] _validReaderIds =
    {
        Guid.Parse("c0000000-0000-0000-0000-000000000001"),
        Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        Guid.Parse("c0000000-0000-0000-0000-000000000003"),
        Guid.Parse("c0000000-0000-0000-0000-000000000004"),
        Guid.Parse("c0000000-0000-0000-0000-000000000005")
    };

    /// <summary>
    /// Генерирует заданное количество батчей Borrow и публикует их в NATS
    /// </summary>
    /// <param name="batchSize">Количество элементов в одном батче</param>
    /// <param name="batchesCount">Количество батчей</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача выполнения операции</returns>
    public async Task GenerateAsync(int batchSize, int batchesCount, CancellationToken cancellationToken = default)
    {
        var faker = new Faker<BorrowCrudDto>()
            .CustomInstantiator(f => new BorrowCrudDto(
                f.PickRandom(_validBookIds),
                f.PickRandom(_validReaderIds),
                DateOnly.FromDateTime(DateTime.UtcNow),
                14,
                null));

        for (var i = 0; i < batchesCount; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            logger.LogInformation(
                "Generating batch {Current}/{Total} (Size: {Size})...",
                i + 1,
                batchesCount,
                batchSize);

            var batch = faker.Generate(batchSize);

            try
            {
                await producer.PublishBatchAsync(batch, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while sending batch {Current}/{Total}", i + 1, batchesCount);
                throw;
            }

            logger.LogInformation(
                "Batch {Current}/{Total} sent successfully ({Size} items)",
                i + 1,
                batchesCount,
                batchSize);

            await Task.Delay(500, cancellationToken);
        }
    }
}