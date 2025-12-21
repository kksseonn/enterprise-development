using System.Text.Json;

namespace Library.Infrastructure.Nats.Serializers;

public static class NatsSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false
    };

    public static byte[] Serialize<T>(IEnumerable<T> data)
    {
        if (data is null)
            return Array.Empty<byte>();

        return JsonSerializer.SerializeToUtf8Bytes(data, _options);
    }
}