namespace Library.Domain.Entities;

/// <summary>
/// Читатель
/// </summary>
public class Reader
{
    /// <summary>
    /// Уникальный идентификатор
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
    /// Адрес проживания читателя
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }
}
