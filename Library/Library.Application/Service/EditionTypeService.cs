using MapsterMapper;
using Library.Application.Contracts.EditionType;
using Library.Domain.IRepository;

namespace Library.Application.Service;

public class EditionTypeService(
    IEditionTypeRepository repository,
    IMapper mapper
) : IEditionTypeReadService
{
    public async Task<EditionTypeDto> Get(Guid id)
    {
        var entity = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"EditionType with ID {id} not found");

        return mapper.Map<EditionTypeDto>(entity);
    }

    public async Task<IList<EditionTypeDto>> GetAll()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<IList<EditionTypeDto>>(entities);
    }

    public async Task<IList<EditionTypeDto>> GetByTypeIdAsync(Guid editionTypeId)
    {
        var entity = await repository.GetByIdAsync(editionTypeId)
            ?? throw new KeyNotFoundException($"EditionType with ID {editionTypeId} not found");

        return new List<EditionTypeDto> { mapper.Map<EditionTypeDto>(entity) };
    }
}
