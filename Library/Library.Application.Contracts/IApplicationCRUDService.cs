using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Contracts;

public interface IApplicationCrudService<TDto, TCreateUpdateDto, TKey>
    : IApplicationReadService<TDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    public Task<TDto> Create(TCreateUpdateDto dto, CancellationToken ct = default);
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId, CancellationToken ct = default);
    public Task<bool> Delete(TKey dtoId, CancellationToken ct = default);
}
