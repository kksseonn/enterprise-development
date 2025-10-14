namespace Library.Domain.Entities;

/// <summary>
/// читатель
/// </summary>
public class Reader
{
    /// <summary>
    /// уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// фамилия читателя
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// имя читателя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// отчество читателя
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// адрес проживания читателя
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// телефон
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// дата регистрации
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }
}
