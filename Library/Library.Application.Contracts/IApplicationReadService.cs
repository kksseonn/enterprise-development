namespace Library.Application.Contracts;

public interface IApplicationReadService<TDto, TKey>
    where TDto : class
    where TKey : struct
{
    public Task<TDto> Get(TKey dtoId);
    public Task<IList<TDto>> GetAll();
}
