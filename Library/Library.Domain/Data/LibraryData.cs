using Library.Domain.Entities;

namespace Library.Domain.Data;

/// <summary>
/// Набор исходных данных для доменной модели библиотеки
/// </summary>
public class LibraryData
{
    /// <summary>
    /// Виды изданий
    /// </summary>
    public static EditionType[] EditionTypes() => new[]
    {
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"), Type = "Учебник" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"), Type = "Монография" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"), Type = "Справочник" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"), Type = "Художественная литература" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"), Type = "Фантастика" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"), Type = "Поэзия" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"), Type = "Научная статья" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000008"), Type = "Энциклопедия" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000009"), Type = "Доклад" },
        new EditionType { Id = Guid.Parse("a0000000-0000-0000-0000-000000000010"), Type = "Комикс" }
    };

    /// <summary>
    /// Издательства
    /// </summary>
    public static Publisher[] Publishers() => new[]
    {
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000001"), Name = "Эксмо" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000002"), Name = "АСТ" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000003"), Name = "Просвещение" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000004"), Name = "Наука" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000005"), Name = "Олимп-Бизнес" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000006"), Name = "МИФ" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000007"), Name = "Дрофа" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000008"), Name = "Азбука" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000009"), Name = "Росмэн" },
        new Publisher { Id = Guid.Parse("b0000000-0000-0000-0000-000000000010"), Name = "Питер" }
    };
}