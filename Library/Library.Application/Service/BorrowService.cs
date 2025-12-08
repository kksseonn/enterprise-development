using Library.Application.Contracts;
using Library.Application.Contracts.Borrow;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с выдачами книг, реализует чтение и CRUD операции
/// </summary>
public class BorrowService :
    IBorrowReadService,
    IApplicationCrudService<BorrowDto, BorrowDto, Guid>
{
    private readonly IRepository<Borrow> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор сервиса
    /// </summary>
    /// <param name="repository">Репозиторий выдач книг</param>
    /// <param name="mapper">Маппер для DTO и сущностей</param>
    public BorrowService(IRepository<Borrow> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Borrow record with ID {id} not found");

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
    public async Task<BorrowDto> Create(BorrowDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Borrow>(dto);
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
    public async Task<BorrowDto> Update(BorrowDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Borrow>(dto);
        entity.Id = dtoId;

        var updated = await _repository.Update(entity, ct);

        return updated == null
            ? throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found")
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