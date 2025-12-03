namespace Library.Application.Contracts.EditionType;

public interface IEditionTypeReadService : IApplicationReadService<EditionTypeDto, Guid>
{
    public Task<IList<EditionTypeDto>> GetByTypeIdAsync(Guid editionTypeId);
}
