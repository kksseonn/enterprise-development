using Library.Application.Contracts.EditionType;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Service;

public class EditionTypeService : IEditionTypeReadService
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
}
