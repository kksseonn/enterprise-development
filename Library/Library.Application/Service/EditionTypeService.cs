using Library.Application.Contracts.EditionType;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

public class EditionTypeService(
    IRepository<EditionType> repository,
    IMapper mapper
) : IEditionTypeReadService
{
    public async Task<EditionTypeDto> Get(Guid id)
    {
        var entity = await repository.Get(id)
            ?? throw new KeyNotFoundException($"EditionType with ID {id} not found");

        return mapper.Map<EditionTypeDto>(entity);
    }

    public async Task<IList<EditionTypeDto>> GetAll() => mapper.Map<List<EditionTypeDto>>(await repository.GetAll());
 
}
