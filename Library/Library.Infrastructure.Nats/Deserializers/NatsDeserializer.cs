using System.Buffers;
using System.Text.Json;

namespace Library.Infrastructure.Nats.Deserializers;

/// <summary>
/// Десериализатор данных, полученных из NATS
/// </summary>
public static class NatsDeserializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Десериализует массив байт в список объектов указанного типа
    /// </summary>
    /// <typeparam name="T">Тип десериализуемых объектов</typeparam>
    /// <param name="sequence">Сегмент байт для десериализации</param>
    /// <returns>Список объектов или null при ошибке</returns>
    public static IList<T>? Deserialize<T>(ReadOnlySequence<byte> sequence)
    {
        if (sequence.IsEmpty)
            return null;

        try
        {
            var reader = new Utf8JsonReader(sequence);
            return JsonSerializer.Deserialize<IList<T>>(ref reader, _options);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}