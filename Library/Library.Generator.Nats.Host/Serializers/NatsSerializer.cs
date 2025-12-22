using System.Text.Json;

namespace Library.Generator.Nats.Host.Serializers;
/// <summary>
/// Сериализатор данных для передачи сообщений через NATS
/// </summary>
public static class NatsSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false
    };

    /// <summary>
    /// Сериализует коллекцию объектов в массив байт для отправки в NATS
    /// </summary>
    /// <typeparam name="T">Тип сериализуемых данных</typeparam>
    /// <param name="data">Коллекция данных для сериализации</param>
    /// <returns>Массив байт с сериализованными данными</returns>
    public static byte[] Serialize<T>(IReadOnlyCollection<T> data)
    {
        if (data is null)
            return Array.Empty<byte>();

        return JsonSerializer.SerializeToUtf8Bytes(data, _options);
    }
}
