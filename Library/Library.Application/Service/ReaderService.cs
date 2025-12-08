using Library.Application.Contracts.Borrow;
using Library.Application.Contracts.Reader;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с читателями, реализует чтение и CRUD операции
/// </summary>
public class ReaderService(IRepository<Reader> _repository, IMapper _mapper) : IReaderCrudService
{

    /// <summary>
    /// Получает читателя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO читателя</returns>
    /// <exception cref="KeyNotFoundException">Если читатель не найден</exception>
    public async Task<ReaderDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Reader with ID {id} not found");

        return _mapper.Map<ReaderDto>(entity);
    }

    /// <summary>
    /// Получает всех читателей
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO читателей</returns>
    public async Task<IReadOnlyList<ReaderDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<ReaderDto>>(entities);
    }

    /// <summary>
    /// Создает нового читателя
    /// </summary>
    /// <param name="dto">DTO читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO читателя</returns>
    public async Task<ReaderDto> Create(ReaderCrudDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Reader>(dto);

        entity.RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var created = await _repository.Create(entity, ct);
        return _mapper.Map<ReaderDto>(created);
    }

    /// <summary>
    /// Обновляет существующего читателя
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO читателя</returns>
    /// <exception cref="KeyNotFoundException">Если читатель не найден</exception>
    public async Task<ReaderDto> Update(ReaderCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await _repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"Reader with ID {dtoId} not found");

        _mapper.Map(dto, existingEntity);

        var updated = await _repository.Update(existingEntity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Reader with ID {dtoId} not found after update");

        return _mapper.Map<ReaderDto>(updated!);
    }

    /// <summary>
    /// Удаляет читателя по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }

    public async Task<IReadOnlyList<BorrowDto>> GetBorrows(Guid readerId, CancellationToken ct = default)
    {
        var entity = await _repository.Get(
            readerId,
            ct,
            includes: r => r.Borrows!
        ) ?? throw new KeyNotFoundException($"Entity with ID {readerId} not found");

        return _mapper.Map<List<BorrowDto>>(entity.Borrows!);
    }
}