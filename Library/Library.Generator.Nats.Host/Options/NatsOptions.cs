namespace Library.Generator.Nats.Host.Options;

/// <summary>
/// Настройки подключения к NATS и JetStream
/// </summary>
public class NatsOptions
{
    /// <summary>
    /// Название секции конфигурации
    /// </summary>
    public const string SectionName = "Nats";

    /// <summary>
    /// Имя потока (stream) в JetStream
    /// </summary>
    public required string StreamName { get; init; }

    /// <summary>
    /// Имя subject для публикации/подписки
    /// </summary>
    public required string SubjectName { get; init; }
}