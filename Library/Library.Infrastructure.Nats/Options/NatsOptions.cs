namespace Library.Infrastructure.Nats.Options;

public class NatsOptions
{
    public const string SectionName = "Nats";
    public string StreamName { get; set; } = "LIBRARY_STREAM";
    public string SubjectName { get; set; } = "library.borrows.ingest";
}
