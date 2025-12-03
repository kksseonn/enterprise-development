using Library.Domain.Entities;

namespace Library.Domain.Data;

/// <summary>
/// Набор исходных данных для доменной модели библиотеки
/// </summary>
public class LibraryData
{

    public List<EditionType> EditionTypes { get; } = [];

    public LibraryData()
    {
        EditionTypes.AddRange([
            new EditionType { Type = "Учебник" },
            new EditionType { Type = "Монография" },
            new EditionType { Type = "Справочник" },
            new EditionType { Type = "Художественная литература" },
            new EditionType { Type = "Фантастика" },
            new EditionType { Type = "Поэзия" },
            new EditionType { Type = "Научная статья" },
            new EditionType { Type = "Энциклопедия" },
            new EditionType { Type = "Доклад" },
            new EditionType { Type = "Комикс" }
        ]);
    }
}