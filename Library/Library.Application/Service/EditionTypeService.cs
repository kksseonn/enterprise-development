using Library.Application.Contracts;
using Library.Application.Contracts.EditionType;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с типами изданий, реализует чтение и CRUD операции
/// </summary>
public class EditionTypeService :
    IEditionTypeReadService,
    IApplicationCrudService<EditionTypeDto, EditionTypeDto, Guid>
{
    private readonly IRepository<EditionType> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор сервиса
    /// </summary>
    /// <param name="repository">Репозиторий типов изданий</param>
    /// <param name="mapper">Маппер для DTO и сущностей</param>
    public EditionTypeService(IRepository<EditionType> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"EditionType with ID {id} not found");

        return _mapper.Map<EditionTypeDto>(entity);
    }

    /// <summary>
    /// Получает все типы изданий
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO типов изданий</returns>
    public async Task<IReadOnlyList<EditionTypeDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<EditionTypeDto>>(entities);
    }

    /// <summary>
    /// Создает новый тип издания
    /// </summary>
    /// <param name="dto">DTO типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO типа издания</returns>
    public async Task<EditionTypeDto> Create(EditionTypeDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<EditionType>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<EditionTypeDto>(created);
    }

    /// <summary>
    /// Обновляет существующий тип издания
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO типа издания</returns>
    /// <exception cref="KeyNotFoundException">Если тип издания не найден</exception>
    public async Task<EditionTypeDto> Update(EditionTypeDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<EditionType>(dto);
        entity.Id = dtoId;
        var updated = await _repository.Update(entity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"EditionType with ID {dtoId} not found");

        return _mapper.Map<EditionTypeDto>(updated);
    }

    /// <summary>
    /// Удаляет тип издания по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор типа издания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}