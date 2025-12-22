using Bogus;
using Library.Application.Contracts.Borrow;
using Library.Infrastructure.Nats;
using Microsoft.Extensions.Logging;

namespace Library.Generator.Nats.Host.Services;

public class BorrowsGenerator : IBorrowsGenerator
{
    private readonly LibraryJetStreamProducer _producer;
    private readonly ILogger<BorrowsGenerator> _logger;

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

    public BorrowsGenerator(
        LibraryJetStreamProducer producer,
        ILogger<BorrowsGenerator> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public async Task GenerateAsync(
        int batchSize,
        int batchesCount,
        CancellationToken cancellationToken = default)
    {
        var faker = new Faker<BorrowCrudDto>()
            .CustomInstantiator(f => new BorrowCrudDto(
                f.PickRandom(_validBookIds),             
                f.PickRandom(_validReaderIds),           
                DateOnly.FromDateTime(DateTime.UtcNow),  
                14,                                      
                null                                     
            ));


        for (var i = 0; i < batchesCount; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var batch = faker.Generate(batchSize);

            await _producer.PublishBatchWithRetryAsync(batch, cancellationToken);

            _logger.LogInformation(
                "Отправлен батч {Current}/{Total} ({Size} эл.)",
                i + 1, batchesCount, batchSize);
        }
    }
}