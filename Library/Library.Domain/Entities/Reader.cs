using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

/// <summary>
/// Читатель
/// </summary>
[Table("readers")]
public class Reader
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Фамилия читателя
    /// </summary>
    [Column("surname")]
    public required string Surname { get; set; }

    /// <summary>
    /// Имя читателя
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Отчество читателя
    /// </summary>
    [Column("patronymic")]
    public string? Patronymic { get; set; }

    /// <summary>
    /// Адрес проживания читателя
    /// </summary>
    [Column("address")]
    public required string Address { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    [Column("phone")]
    public required string Phone { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    [Column("registration_date")]
    public required DateOnly RegistrationDate { get; set; }

    /// <summary>
    /// Список записей о выдаче
    /// </summary>
    public virtual List<Borrow>? Borrows { get; set; } = new List<Borrow>();
}
