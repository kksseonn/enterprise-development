using Library.Application.Contracts.Book;
using Library.Application.Contracts.Publisher;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с издательствами, реализует чтение и CRUD операции
/// </summary>
public class PublisherService(IRepository<Publisher> _repository, IMapper _mapper) : IPublisherCrudService
{
    /// <summary>
    /// Получает издателя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор издателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO издателя</returns>
    /// <exception cref="KeyNotFoundException">Если издатель не найден</exception>
    public async Task<PublisherDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Publisher with ID {id} not found");

        return _mapper.Map<PublisherDto>(entity);
    }

    /// <summary>
    /// Получает всех издательств
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO издательств</returns>
    public async Task<IReadOnlyList<PublisherDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<PublisherDto>>(entities);
    }

    /// <summary>
    /// Создает новое издательство
    /// </summary>
    /// <param name="dto">DTO издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO издательства</returns>
    public async Task<PublisherDto> Create(PublisherCrudDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Publisher>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<PublisherDto>(created);
    }

    /// <summary>
    /// Обновляет существующее издательство
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO издательства</returns>
    /// <exception cref="KeyNotFoundException">Если издательство не найден</exception>
    public async Task<PublisherDto> Update(PublisherCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await _repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"Publisher with ID {dtoId} not found");

        _mapper.Map(dto, existingEntity);

        var updated = await _repository.Update(existingEntity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Publisher with ID {dtoId} not found after update");

        return _mapper.Map<PublisherDto>(updated);
    }

    /// <summary>
    /// Удаляет издательство по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }


    /// <summary>
    /// Получить все книги, связанные с конкретным издателем
    /// </summary>
    /// <param name="publisherId">Идентификатор издателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг, принадлежащих издателю</returns>
    /// <exception cref="KeyNotFoundException">Если издатель не найден</exception>
    public async Task<IReadOnlyList<BookDto>> GetBooks(Guid publisherId, CancellationToken ct = default)
    {
        var entity = await _repository.Get(
            publisherId,
            ct,
            includes: p => p.Books!
        ) ?? throw new KeyNotFoundException($"Entity with ID {publisherId} not found");

        return _mapper.Map<List<BookDto>>(entity.Books!);
    }
}