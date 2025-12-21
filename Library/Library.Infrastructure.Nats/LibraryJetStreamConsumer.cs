using Library.Application.Contracts;
using Library.Infrastructure.Nats.Deserializers;
using Library.Infrastructure.Nats.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;
using System.Buffers;

public abstract class LibraryJetStreamConsumer<TDto, TCrudDto, TKey>(
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    ILogger logger,
    IOptions<NatsOptions> options,
    string consumerName)
    : BackgroundService
    where TDto : class
    where TCrudDto : class
    where TKey : struct
{
    private readonly NatsOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation(
                    "NATS Consumer {ConsumerName} is connecting to JetStream...",
                    consumerName);

                var js = connection.CreateJetStreamContext();

                await js.CreateOrUpdateStreamAsync(
                    new StreamConfig(
                        name: _options.StreamName,
                        subjects: [_options.SubjectName]
                    ),
                    stoppingToken);

                var consumer = await js.CreateOrUpdateConsumerAsync(
                    _options.StreamName,
                    new ConsumerConfig
                    {
                        Name = consumerName,
                        DurableName = consumerName,
                        AckPolicy = ConsumerConfigAckPolicy.Explicit,
                        AckWait = TimeSpan.FromSeconds(30),
                        MaxAckPending = 1000
                    },
                    stoppingToken);

                logger.LogInformation(
                    "NATS Consumer {ConsumerName} connected. Stream={Stream}, Subject={Subject}",
                    consumerName,
                    _options.StreamName,
                    _options.SubjectName);

                await foreach (var msg in consumer.ConsumeAsync<byte[]>(cancellationToken: stoppingToken))
                {
                    var batch = NatsDeserializer.Deserialize<TCrudDto>(
                        new ReadOnlySequence<byte>(msg.Data!)
                    );

                    if (batch is null || batch.Count == 0)
                    {
                        logger.LogError(
                            "Invalid or empty batch received in {ConsumerName}. Message skipped.",
                            consumerName);

                        await msg.AckAsync(cancellationToken: stoppingToken);
                        continue;
                    }

                    var processedSuccessfully = true;

                    using var scope = scopeFactory.CreateScope();
                    var service = scope.ServiceProvider
                        .GetRequiredService<IApplicationCrudService<TDto, TCrudDto, TKey>>();

                    foreach (var item in batch)
                    {
                        try
                        {
                            await service.Create(item, stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            processedSuccessfully = false;

                            logger.LogError(
                                ex,
                                "Failed to process item in {ConsumerName}",
                                consumerName);

                            break;
                        }
                    }

                    if (processedSuccessfully)
                        await msg.AckAsync(cancellationToken: stoppingToken);
                    else
                        await msg.NakAsync(cancellationToken: stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "NATS error in {ConsumerName}. Retrying in 5 seconds...",
                    consumerName);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}