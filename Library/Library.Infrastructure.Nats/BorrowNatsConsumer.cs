using System.Buffers;
using Library.Application.Contracts.Borrow;
using Library.Infrastructure.Nats.Deserializers;
using Library.Infrastructure.Nats.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;

/// <summary>
/// Служба для чтения данных из сабжекта NATS при помощи Push-консьюмера
/// </summary>
/// <param name="connection">Подключение к NATS</param>
/// <param name="scopeFactory">Фабрика контекста для создания Scope</param>
/// <param name="options">Настройки конфигурации NATS</param>
/// <param name="logger">Логгер</param>
public sealed class BorrowNatsConsumer(
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    IOptions<NatsOptions> options,
    ILogger<BorrowNatsConsumer> logger)
    : BackgroundService
{
    private readonly NatsOptions _options = options.Value;

    /// <summary>
    /// Основной цикл выполнения фоновой службы
    /// </summary>
    /// <param name="stoppingToken">Токен отмены операции</param>
    /// <returns>Задача, представляющая выполнение службы</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Starting NATS Consumer: {ConsumerName}", _options.ConsumerName);
                await connection.ConnectAsync();

                var jetStream = connection.CreateJetStreamContext();

                await jetStream.CreateOrUpdateStreamAsync(
                    new StreamConfig(_options.StreamName, [_options.SubjectName]),
                    stoppingToken);

                var consumer = await jetStream.CreateOrUpdateConsumerAsync(
                    _options.StreamName,
                    new ConsumerConfig
                    {
                        Name = _options.ConsumerName,
                        DurableName = _options.ConsumerName,
                        AckPolicy = ConsumerConfigAckPolicy.Explicit,
                        FilterSubject = _options.SubjectName,
                        AckWait = TimeSpan.FromSeconds(30),
                        MaxAckPending = 1000
                    },
                    stoppingToken);

                await foreach (var message in consumer.ConsumeAsync<byte[]>(cancellationToken: stoppingToken))
                {
                    try
                    {
                        if (message.Data is null || message.Data.Length == 0)
                        {
                            await message.AckAsync(cancellationToken: stoppingToken);
                            continue;
                        }

                        var batch = NatsDeserializer.Deserialize<BorrowCrudDto>(
                            new ReadOnlySequence<byte>(message.Data));

                        if (batch == null || batch.Count == 0)
                        {
                            await message.AckAsync(cancellationToken: stoppingToken);
                            continue;
                        }

                        using var scope = scopeFactory.CreateScope();
                        var borrowService = scope.ServiceProvider.GetRequiredService<IBorrowCrudService>();

                        foreach (var item in batch)
                        {
                            try
                            {
                                await borrowService.Create(item, stoppingToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogWarning(ex, "Failed to process borrow item in batch");
                            }
                        }

                        await message.AckAsync(cancellationToken: stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error during message processing. Sending NAK");
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
                logger.LogError(ex, "NATS Consumer critical error. Retrying in 5s...");
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (OperationCanceledException) { break; }
            }
        }
    }
}