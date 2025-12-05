using Library.Application.Contracts;
using Library.Application.Contracts.Borrow;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

public class BorrowService :
    IBorrowReadService,
    IApplicationCrudService<BorrowDto, BorrowDto, Guid>
{
    private readonly IRepository<Borrow> _repository;
    private readonly IMapper _mapper;

    public BorrowService(IRepository<Borrow> repository,
                        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BorrowDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Borrow record with ID {id} not found");

        return _mapper.Map<BorrowDto>(entity);
    }

    public async Task<IReadOnlyList<BorrowDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<BorrowDto>>(entities);
    }

    public async Task<BorrowDto> Create(BorrowDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Borrow>(dto);
        var created = await _repository.Create(entity, ct);

        return _mapper.Map<BorrowDto>(created);
    }

    public async Task<BorrowDto> Update(BorrowDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Borrow>(dto);
        entity.Id = dtoId;

        var updated = await _repository.Update(entity, ct);

        return updated == null
            ? throw new KeyNotFoundException($"Borrow record with ID {dtoId} not found")
            : _mapper.Map<BorrowDto>(updated);
    }

    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}