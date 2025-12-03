namespace Library.Application.Contracts;

public interface IApplicationReadService<TDto, TKey>
    where TDto : class
    where TKey : struct
{
    public Task<TDto> Get(TKey dtoId, CancellationToken ct = default);
    public Task<IReadOnlyList<TDto>> GetAll(CancellationToken ct = default);
}
