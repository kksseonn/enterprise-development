using Library.Generator.Nats.Host.Interfaces;
using Library.Generator.Nats.Host.Serializers;
using Library.Infrastructure.Nats.Options;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Net;

namespace Library.Generator.Nats.Host;

/// <summary>
/// Сервис для публикации батчей данных в NATS JetStream с повторными попытками при ошибках
/// </summary>
public sealed class LibraryNatsProducer(
    INatsConnection connection,
    IOptions<NatsOptions> options,
    ILogger<LibraryNatsProducer> logger)
    : IProducerService
{
    private const int MaxRetries = 3;
    private const int InitialDelayMs = 500;

    /// <summary>
    /// Публикует батч данных в NATS
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <param name="batch">Батч данных для публикации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача выполнения операции</returns>
    public async Task PublishBatchAsync<T>(IReadOnlyCollection<T> batch, CancellationToken cancellationToken = default)
    {
        if (batch.Count == 0)
        {
            logger.LogWarning(
                "Попытка отправить пустой батч в subject {Subject} проигнорирована",
                options.Value.SubjectName);
            return;
        }

        var js = connection.CreateJetStreamContext();
        var payload = NatsSerializer.Serialize(batch);

        var attempt = 0;
        var delay = InitialDelayMs;

        while (true)
        {
            attempt++;

            try
            {
                await js.PublishAsync(
                    options.Value.SubjectName,
                    payload,
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                logger.LogInformation(
                    "Отправлен батч ({Count} эл.) в {Subject}. Попытка {Attempt}",
                    batch.Count,
                    options.Value.SubjectName,
                    attempt);

                return;
            }
            catch (Exception ex) when (attempt < MaxRetries)
            {
                logger.LogWarning(
                    ex,
                    "Ошибка публикации в {Subject}. Повтор через {Delay}мс ({Attempt}/{Max})",
                    options.Value.SubjectName,
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
                    "Не удалось отправить батч в {Subject} после {Max} попыток",
                    options.Value.SubjectName,
                    MaxRetries);

                throw;
            }
        }
    }
}