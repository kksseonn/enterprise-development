namespace Library.Domain.Entities;

/// <summary>
/// вид издания
/// </summary>
public class EditionType
{
    public int Id { get; set; }

    /// <summary>
    /// название издательства
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
