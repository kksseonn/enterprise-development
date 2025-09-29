namespace Library.Domain.Entities;

/// <summary>
/// читатель
/// </summary>
public class Reader
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО читателя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// телефон
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// дата регистрации
    /// </summary>
    public required DateTime RegistrationDate { get; set; }
}
