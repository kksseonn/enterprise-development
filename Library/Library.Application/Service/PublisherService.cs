using Library.Application.Contracts;
using Library.Application.Contracts.Publisher;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

public class PublisherService :
    IPublisherReadService,
    IApplicationCrudService<PublisherDto, PublisherDto, Guid>
{
    private readonly IRepository<Publisher> _repository;
    private readonly IMapper _mapper;

    public PublisherService(IRepository<Publisher> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PublisherDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Publisher with ID {id} not found");

        return _mapper.Map<PublisherDto>(entity);
    }

    public async Task<IReadOnlyList<PublisherDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<PublisherDto>>(entities);
    }

    public async Task<PublisherDto> Create(PublisherDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Publisher>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<PublisherDto>(created);
    }

    public async Task<PublisherDto> Update(PublisherDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Publisher>(dto);
        entity.Id = dtoId;
        var updated = await _repository.Update(entity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Publisher with ID {dtoId} not found");

        return _mapper.Map<PublisherDto>(updated);
    }

    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}
