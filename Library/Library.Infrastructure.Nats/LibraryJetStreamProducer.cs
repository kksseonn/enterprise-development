using Library.Infrastructure.Nats.Options;
using Library.Infrastructure.Nats.Serializers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Net;

namespace Library.Infrastructure.Nats;

public class LibraryJetStreamProducer(
    INatsConnection connection,
    IOptions<NatsOptions> options,
    ILogger<LibraryJetStreamProducer> logger)
{
    public async Task PublishBatchWithRetryAsync<T>(
        IEnumerable<T> data,
        CancellationToken ct = default,
        int maxRetries = 3,
        int initialDelayMs = 500)
    {
        var natsOptions = options.Value;

        var dataList = data?.ToList();

        if (dataList is null || dataList.Count == 0)
        {
            logger.LogWarning("Попытка отправить пустой батч в subject {Subject} проигнорирована.", natsOptions.SubjectName);
            return;
        }

        var js = connection.CreateJetStreamContext();
        var payload = NatsSerializer.Serialize(dataList);

        var attempt = 0;
        var delayMs = initialDelayMs;

        while (true)
        {
            attempt++;
            try
            {
                await js.PublishAsync(
                    subject: natsOptions.SubjectName,
                    data: payload,
                    cancellationToken: ct);

                logger.LogInformation(
                    "Успешно отправлен батч ({Count} эл.) в {Subject}. Попытка: {Attempt}",
                    dataList.Count, natsOptions.SubjectName, attempt);

                break;
            }
            catch (Exception ex)
            {
                if (attempt >= maxRetries)
                {
                    logger.LogError(ex, "Превышено число попыток ({Max}) отправки в {Subject}.", maxRetries, natsOptions.SubjectName);
                    throw;
                }

                logger.LogWarning("Ошибка публикации в {Subject}. Повтор через {Delay}мс... ({Attempt}/{Max})",
                    natsOptions.SubjectName, delayMs, attempt, maxRetries);

                await Task.Delay(delayMs, ct);
                delayMs *= 2;
            }
        }
    }
}