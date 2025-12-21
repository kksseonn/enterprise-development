using System.Buffers;
using System.Text.Json;

namespace Library.Infrastructure.Nats.Deserializers;

public static class NatsDeserializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

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