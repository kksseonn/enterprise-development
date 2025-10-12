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
    /// Фамилия читателя
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Имя читателя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Отчество читателя
    /// </summary>
    public string? Patronymic { get; set; }

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
    public required DateOnly RegistrationDate { get; set; }
}
