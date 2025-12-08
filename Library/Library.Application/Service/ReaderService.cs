using Library.Application.Contracts;
using Library.Application.Contracts.Reader;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с читателями, реализует чтение и CRUD операции
/// </summary>
public class ReaderService :
    IReaderReadService,
    IApplicationCrudService<ReaderDto, ReaderDto, Guid>
{
    private readonly IRepository<Reader> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор сервиса
    /// </summary>
    /// <param name="repository">Репозиторий читателей</param>
    /// <param name="mapper">Маппер для DTO и сущностей</param>
    public ReaderService(IRepository<Reader> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

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
    public async Task<ReaderDto> Create(ReaderDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Reader>(dto);
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
    public async Task<ReaderDto> Update(ReaderDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Reader>(dto);
        entity.Id = dtoId;
        var updated = await _repository.Update(entity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Reader with ID {dtoId} not found");

        return _mapper.Map<ReaderDto>(updated);
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
}