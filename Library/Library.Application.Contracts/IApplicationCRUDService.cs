namespace Library.Application.Contracts;

public interface IApplicationCrudService<TDto, TCreateUpdateDto, TKey> : IApplicationReadService<TDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    public Task<TDto> Create(TCreateUpdateDto dto);
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);
    public Task<bool> Delete(TKey dtoId);
}
