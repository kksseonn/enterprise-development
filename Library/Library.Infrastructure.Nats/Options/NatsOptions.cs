namespace Library.Infrastructure.Nats.Options;

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
    public string StreamName { get; set; } = "LIBRARY_STREAM";

    /// <summary>
    /// Имя subject для публикации/подписки
    /// </summary>
    public string SubjectName { get; set; } = "library.borrows.ingest";
}