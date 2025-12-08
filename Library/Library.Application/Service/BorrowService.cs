using Library.Application.Contracts;
using Library.Application.Contracts.Borrow;
using Library.Application.Contracts.Publisher;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с выдачами книг, реализует чтение и CRUD операции
/// </summary>
public class BorrowService(IRepository<Borrow> _repository, IMapper _mapper) : IBorrowCrudService
{

    /// <summary>
    /// Получает запись о выдаче по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO записи о выдаче</returns>
    /// <exception cref="KeyNotFoundException">Если запись не найдена</exception>
    public async Task<BorrowDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(
            id,
            ct,
            includes: [b => b.Book!, b => b.Reader!]
        ) ?? throw new KeyNotFoundException($"Borrow record with ID {id} not found");

        return _mapper.Map<BorrowDto>(entity);
    }

    /// <summary>
    /// Получает все записи о выдачах
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO выдач</returns>
    public async Task<IReadOnlyList<BorrowDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<BorrowDto>>(entities);
    }

    /// <summary>
    /// Создает новую запись о выдаче
    /// </summary>
    /// <param name="dto">DTO записи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO записи</returns>
    public async Task<BorrowDto> Create(BorrowCrudDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Borrow>(dto);
        entity.DueDate = entity.BorrowDate.AddDays(entity.Days);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<BorrowDto>(created);
    }

    /// <summary>
    /// Обновляет существующую запись о выдаче
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор записи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO записи</returns>
    /// <exception cref="KeyNotFoundException">Если запись не найдена</exception>
    public async Task<BorrowDto> Update(BorrowCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await _repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found");

        _mapper.Map(dto, existingEntity);

        existingEntity.DueDate = existingEntity.BorrowDate.AddDays(existingEntity.Days);

        var updated = await _repository.Update(existingEntity, ct);

        return updated == null
            ? throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found after update")
            : _mapper.Map<BorrowDto>(updated);
    }

    /// <summary>
    /// Удаляет запись о выдаче по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор записи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}