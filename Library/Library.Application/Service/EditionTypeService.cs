using Library.Application.Contracts.Book;
using Library.Application.Contracts.EditionType;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с типами изданий, реализует чтение и CRUD операции
/// </summary>
public class EditionTypeService(IRepository<EditionType> repository, IMapper mapper) : IEditionTypeCrudService
{
    /// <summary>
    /// Получает все типы изданий
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO типов изданий</returns>
    public async Task<IReadOnlyList<EditionTypeDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await repository.GetAll(ct);
        return mapper.Map<List<EditionTypeDto>>(entities);
    }

    /// <summary>
    /// Получает тип издания по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO типа издания</returns>
    /// <exception cref="KeyNotFoundException">Если тип издания не найден</exception>
    public async Task<EditionTypeDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"EditionType with ID {id} not found");

        return mapper.Map<EditionTypeDto>(entity);
    }

    /// <summary>
    /// Создает новый тип издания
    /// </summary>
    /// <param name="dto">DTO типа издания для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO типа издания</returns>
    public async Task<EditionTypeDto> Create(EditionTypeCrudDto dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<EditionType>(dto);
        var created = await repository.Create(entity, ct);
        return mapper.Map<EditionTypeDto>(created);
    }

    /// <summary>
    /// Обновляет существующий тип издания
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO типа издания</returns>
    /// <exception cref="KeyNotFoundException">Если тип издания не найден</exception>
    public async Task<EditionTypeDto> Update(EditionTypeCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"EditionType with ID {dtoId} not found");

        mapper.Map(dto, existingEntity);

        var updated = await repository.Update(existingEntity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"EditionType with ID {dtoId} not found during update");

        return mapper.Map<EditionTypeDto>(updated);
    }

    /// <summary>
    /// Удаляет тип издания по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await repository.Delete(dtoId, ct);
    }

    /// <summary>
    /// Получить все книги, связанные с конкретным типом издания
    /// </summary>
    /// <param name="editionTypeId">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг <see cref="BookDto"/></returns>
    /// <exception cref="KeyNotFoundException">Если тип издания не найден</exception>
    public async Task<IReadOnlyList<BookDto>> GetBooks(Guid editionTypeId, CancellationToken ct = default)
    {
        var entity = await repository.Get(
            editionTypeId,
            ct,
            includes: e => e.Books!
        ) ?? throw new KeyNotFoundException($"EditionType with ID {editionTypeId} not found");

        return mapper.Map<List<BookDto>>(entity.Books!);
    }
}