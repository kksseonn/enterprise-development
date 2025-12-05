using Library.Application.Contracts;
using Library.Application.Contracts.Book;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

public class BookService :
    IBookReadService,
    IApplicationCrudService<BookDto, BookDto, Guid>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public BookService(IRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Book with ID {id} not found");
        return _mapper.Map<BookDto>(entity);
    }
    public async Task<IReadOnlyList<BookDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<BookDto>>(entities);
    }

    public async Task<BookDto> Create(BookDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Book>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<BookDto>(created);
    }

    public async Task<BookDto> Update(BookDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Book>(dto);
        entity.Id = dtoId;

        var updated = await _repository.Update(entity, ct);

        if (updated == null)
            throw new KeyNotFoundException($"Book with ID {dtoId} not found");

        return _mapper.Map<BookDto>(updated);
    }

    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}