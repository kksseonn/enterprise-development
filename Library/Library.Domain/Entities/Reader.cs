namespace Library.Domain.Entities;

/// <summary>
/// читатель
/// </summary>
public class Reader
{
    public int Id { get; set; }

    /// <summary>
    /// ФИО читателя
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// адрес
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// телефон
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// дата регистрации
    /// </summary>
    public DateTime RegistrationDate { get; set; }
}
