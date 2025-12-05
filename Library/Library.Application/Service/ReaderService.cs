using Library.Application.Contracts;
using Library.Application.Contracts.Reader;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

public class ReaderService :
    IReaderReadService,
    IApplicationCrudService<ReaderDto, ReaderDto, Guid>
{
    private readonly IRepository<Reader> _repository;
    private readonly IMapper _mapper;

    public ReaderService(IRepository<Reader> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReaderDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Reader with ID {id} not found");

        return _mapper.Map<ReaderDto>(entity);
    }

    public async Task<IReadOnlyList<ReaderDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<ReaderDto>>(entities);
    }

    public async Task<ReaderDto> Create(ReaderDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Reader>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<ReaderDto>(created);
    }

    public async Task<ReaderDto> Update(ReaderDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Reader>(dto);
        entity.Id = dtoId;
        var updated = await _repository.Update(entity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Reader with ID {dtoId} not found");

        return _mapper.Map<ReaderDto>(updated);
    }

    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}
