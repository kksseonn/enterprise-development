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

/// <summary>
/// Базовый consumer для обработки сообщений из NATS JetStream
/// </summary>
/// <typeparam name="TDto">DTO, возвращаемый API</typeparam>
/// <typeparam name="TCrudDto">DTO для создания сущности</typeparam>
/// <typeparam name="TKey">Тип первичного ключа</typeparam>
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

    /// <summary>
    /// Основной цикл обработки сообщений из JetStream
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation(
                    "NATS consumer {ConsumerName} connecting to JetStream",
                    consumerName);

                var jetStream = connection.CreateJetStreamContext();

                var consumer = await jetStream.CreateOrUpdateConsumerAsync(
                    _options.StreamName,
                    new ConsumerConfig
                    {
                        Name = consumerName,
                        DurableName = consumerName,
                        AckPolicy = ConsumerConfigAckPolicy.Explicit,
                        AckWait = TimeSpan.FromSeconds(30),
                        MaxAckPending = 1000,
                        FilterSubject = _options.SubjectName
                    },
                    stoppingToken);

                logger.LogInformation(
                    "NATS consumer {ConsumerName} connected. Stream={Stream}, Subject={Subject}",
                    consumerName,
                    _options.StreamName,
                    _options.SubjectName);

                await foreach (var message in consumer.ConsumeAsync<byte[]>(
                                   cancellationToken: stoppingToken))
                {
                    try
                    {
                        logger.LogInformation(
                            "Message received from NATS. Size={Size} bytes",
                            message.Data?.Length ?? 0);

                        if (message.Data is null || message.Data.Length == 0)
                        {
                            await message.AckAsync(cancellationToken: stoppingToken);
                            continue;
                        }

                        var batch = NatsDeserializer.Deserialize<TCrudDto>(
                            new ReadOnlySequence<byte>(message.Data));

                        if (batch is null || batch.Count == 0)
                        {
                            logger.LogWarning(
                                "Empty or invalid batch received in {ConsumerName}. Message skipped",
                                consumerName);

                            await message.AckAsync(cancellationToken: stoppingToken);
                            continue;
                        }

                        logger.LogInformation(
                            "Batch deserialized. Items count={Count}",
                            batch.Count);

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
                                logger.LogWarning(
                                    ex,
                                    "Invalid item skipped in {ConsumerName}",
                                    consumerName);
                            }
                        }

                        await message.AckAsync(cancellationToken: stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "Technical failure while processing message in {ConsumerName}",
                            consumerName);

                        await message.NakAsync(cancellationToken: stoppingToken);
                    }
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
                    "NATS consumer {ConsumerName} error. Retrying in 5 seconds",
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