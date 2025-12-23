using Library.Application.Contracts.Borrow;
using Library.Infrastructure.Nats.Options;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;
using System.Text.Json;

namespace Library.Generator.Nats.Host;

/// <summary>
/// Сервис для публикации батчей данных в NATS JetStream с повторными попытками при ошибках
/// </summary>
public sealed class BorrowNatsProducer(
    INatsConnection connection,
    IOptions<NatsOptions> options,
    ILogger<BorrowNatsProducer> logger)
{
    private readonly NatsOptions _options = options.Value;
    private const int MaxRetries = 3;
    private const int InitialDelayMs = 500;

    /// <summary>
    /// Публикует батч данных в NATS
    /// </summary>
    /// <param name="batch">Батч данных для публикации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача выполнения операции</returns>
    public async Task PublishBatchAsync(IList<BorrowCrudDto> batch, CancellationToken cancellationToken = default)
    {
        if (batch.Count == 0)
        {
            logger.LogWarning(
                "Empty batch send attempt to subject {Subject} ignored",
                _options.SubjectName);
            return;
        }

        var js = connection.CreateJetStreamContext();

        await js.CreateOrUpdateStreamAsync(new StreamConfig(
            name: _options.StreamName,
            subjects: [_options.SubjectName]),
            cancellationToken: cancellationToken);

        var payload = JsonSerializer.SerializeToUtf8Bytes(batch);
        var attempt = 0;
        var delay = InitialDelayMs;

        while (true)
        {
            attempt++;

            try
            {
                await js.PublishAsync(
                    _options.SubjectName,
                    payload,
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                logger.LogInformation(
                    "Batch sent ({Count} items) to {Subject}. Attempt {Attempt}",
                    batch.Count,
                    _options.SubjectName,
                    attempt);

                return;
            }
            catch (Exception ex) when (attempt < MaxRetries)
            {
                logger.LogWarning(
                    ex,
                    "Publishing error in {Subject}. Retrying in {Delay}ms ({Attempt}/{Max})",
                    _options.SubjectName,
                    delay,
                    attempt,
                    MaxRetries);

                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                delay *= 2;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Failed to send batch to {Subject} after {Max} attempts",
                    _options.SubjectName,
                    MaxRetries);

                throw;
            }
        }
    }
}