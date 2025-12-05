using Library.Application.Contracts;
using Library.Application.Contracts.EditionType;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

public class EditionTypeService :
    IEditionTypeReadService,
    IApplicationCrudService<EditionTypeDto, EditionTypeDto, Guid>
{
    private readonly IRepository<EditionType> _repository;
    private readonly IMapper _mapper;

    public EditionTypeService(IRepository<EditionType> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EditionTypeDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"EditionType with ID {id} not found");

        return _mapper.Map<EditionTypeDto>(entity);
    }

    public async Task<IReadOnlyList<EditionTypeDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<EditionTypeDto>>(entities);
    }

    public async Task<EditionTypeDto> Create(EditionTypeDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<EditionType>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<EditionTypeDto>(created);
    }

    public async Task<EditionTypeDto> Update(EditionTypeDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<EditionType>(dto);
        entity.Id = dtoId;
        var updated = await _repository.Update(entity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"EditionType with ID {dtoId} not found");

        return _mapper.Map<EditionTypeDto>(updated);
    }

    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}
