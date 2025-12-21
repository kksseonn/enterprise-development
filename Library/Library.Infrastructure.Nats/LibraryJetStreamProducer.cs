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
    private readonly NatsOptions _options = options.Value;
    private readonly ILogger _logger = logger;

    public async Task PublishBatchWithRetryAsync<T>(
        IEnumerable<T> data,
        CancellationToken ct = default,
        int maxRetries = 3,
        int initialDelayMs = 500)
    {
        if (data is null || !data.Any())
        {
            _logger.LogWarning("Попытка отправить пустой батч в subject {Subject} проигнорирована.", _options.SubjectName);
            return;
        }

        var js = connection.CreateJetStreamContext();

        var payload = NatsSerializer.Serialize(data);

        var attempt = 0;
        var delayMs = initialDelayMs;

        while (true)
        {
            attempt++;
            try
            {
                await js.PublishAsync(
                    subject: _options.SubjectName,
                    data: payload,
                    cancellationToken: ct);

                _logger.LogInformation(
                    "Успешно отправлен батч ({Count} эл.) в {Subject}. Попытка: {Attempt}",
                    data.Count(), _options.SubjectName, attempt);

                break;
            }
            catch (Exception ex)
            {
                if (attempt >= maxRetries)
                {
                    _logger.LogError(ex, "Превышено число попыток ({Max}) отправки в {Subject}.", maxRetries, _options.SubjectName);
                    throw;
                }

                _logger.LogWarning("Ошибка публикации в {Subject}. Повтор через {Delay}мс... ({Attempt}/{Max})",
                    _options.SubjectName, delayMs, attempt, maxRetries);

                await Task.Delay(delayMs, ct);
                delayMs *= 2;
            }
        }
    }
}