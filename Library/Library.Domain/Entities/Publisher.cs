namespace Library.Domain.Entities;

/// <summary>
/// издательство
/// </summary>
public class Publisher
{
    public int Id { get; set; }

    /// <summary>
    /// название издательства
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
