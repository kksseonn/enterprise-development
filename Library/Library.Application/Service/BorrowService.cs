using Library.Application.Contracts;
using Library.Application.Contracts.Borrow;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с выдачами книг, реализует чтение и CRUD операции
/// </summary>
public class BorrowService(IRepository<Borrow> repository, IMapper mapper) : IBorrowCrudService
{
    /// <summary>
    /// Получает все записи о выдачах
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO выдач</returns>
    public async Task<IReadOnlyList<BorrowDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await repository.GetAll(ct);
        return mapper.Map<List<BorrowDto>>(entities);
    }

    /// <summary>
    /// Получает запись о выдаче по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор выдачи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO записи о выдаче</returns>
    /// <exception cref="KeyNotFoundException">Если запись не найдена</exception>
    public async Task<BorrowDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await repository.Get(
            id,
            ct,
            includes: [b => b.Book!, b => b.Reader!]
        ) ?? throw new KeyNotFoundException($"Borrow record with ID {id} not found");

        return mapper.Map<BorrowDto>(entity);
    }

    /// <summary>
    /// Создает новую запись о выдаче
    /// </summary>
    /// <param name="dto">DTO записи для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO записи</returns>
    public async Task<BorrowDto> Create(BorrowCrudDto dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<Borrow>(dto);

        entity.DueDate = entity.BorrowDate.AddDays(entity.Days);

        entity.ReturnDate = null;

        var created = await repository.Create(entity, ct);

        var createdWithIncludes = await repository.Get(
            created.Id,
            ct,
            includes: [b => b.Book!, b => b.Reader!]
        );

        if (createdWithIncludes == null)
        {
            throw new InvalidOperationException($"Failed to retrieve newly created Borrow record with ID {created.Id}");
        }

        return mapper.Map<BorrowDto>(createdWithIncludes);
    }

    /// <summary>
    /// Обновляет существующую запись о выдаче (например, для фиксации даты возврата)
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор записи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO записи</returns>
    /// <exception cref="KeyNotFoundException">Если запись не найдена</exception>
    public async Task<BorrowDto> Update(BorrowCrudDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var existingEntity = await repository.Get(dtoId, ct)
            ?? throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found");

        if (dto.BorrowDate != existingEntity.BorrowDate)
        {
            throw new InvalidOperationException("Cannot modify the BorrowDate after the record has been created.");
        }

        mapper.Map(dto, existingEntity);

        existingEntity.DueDate = existingEntity.BorrowDate.AddDays(existingEntity.Days);

        var updated = await repository.Update(existingEntity, ct);

        if (updated == null)
        {
            throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found during update");
        }

        var resultEntity = await repository.Get(
            dtoId,
            ct,
            includes: [b => b.Book!, b => b.Reader!]
        ) ?? throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found after update");

        return mapper.Map<BorrowDto>(resultEntity);
    }

    /// <summary>
    /// Удаляет запись о выдаче по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор записи</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await repository.Delete(dtoId, ct);
    }
}