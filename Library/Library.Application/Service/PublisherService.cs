using Library.Application.Contracts.Book;
using Library.Application.Contracts.Publisher;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с издательствами, реализует чтение и CRUD операции
/// </summary>
public class PublisherService(IRepository<Publisher> repository, IMapper mapper) : IPublisherCrudService
{
    /// <summary>
    /// Получает все издательства
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO издательств</returns>
    public async Task<IReadOnlyList<PublisherDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await repository.GetAll(ct);
        return mapper.Map<List<PublisherDto>>(entities);
    }

    /// <summary>
    /// Получает издательство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO издательства</returns>
    /// <exception cref="KeyNotFoundException">Если издательство не найдено</exception>
    public async Task<PublisherDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Publisher with ID {id} not found");

        return mapper.Map<PublisherDto>(entity);
    }

    /// <summary>
    /// Создает новое издательство
    /// </summary>
    /// <param name="dto">DTO издательства для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO издательства</returns>
    public async Task<PublisherDto> Create(PublisherCrudDto dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<Publisher>(dto);
        var created = await repository.Create(entity, ct);
        return mapper.Map<PublisherDto>(created);
    }

    /// <summary>
    /// Обновляет существующее издательство
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO издательства</returns>
    /// <exception cref="KeyNotFoundException">Если издательство не найдено</exception>
    public async Task<PublisherDto> Update(PublisherCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"Publisher with ID {dtoId} not found");

        mapper.Map(dto, existingEntity);

        var updated = await repository.Update(existingEntity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Publisher with ID {dtoId} not found during update");

        return mapper.Map<PublisherDto>(updated);
    }

    /// <summary>
    /// Удаляет издательство по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await repository.Delete(dtoId, ct);
    }

    /// <summary>
    /// Получить все книги, связанные с конкретным издательством
    /// </summary>
    /// <param name="publisherId">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг, принадлежащих издательству</returns>
    /// <exception cref="KeyNotFoundException">Если издательство не найдено</exception>
    public async Task<IReadOnlyList<BookDto>> GetBooks(Guid publisherId, CancellationToken ct = default)
    {
        var entity = await repository.Get(
            publisherId,
            ct,
            includes: p => p.Books!
        ) ?? throw new KeyNotFoundException($"Publisher with ID {publisherId} not found");

        return mapper.Map<List<BookDto>>(entity.Books!);
    }
}