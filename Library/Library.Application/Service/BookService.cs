using Library.Application.Contracts;
using Library.Application.Contracts.Book;
using Library.Domain;
using Library.Domain.Entities;
using MapsterMapper;

namespace Library.Application.Service;

/// <summary>
/// Сервис для работы с книгами, реализует чтение и CRUD операции
/// </summary>
public class BookService :
    IBookReadService,
    IApplicationCrudService<BookDto, BookDto, Guid>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор сервиса
    /// </summary>
    /// <param name="repository">Репозиторий книг</param>
    /// <param name="mapper">Маппер для DTO и сущностей</param>
    public BookService(IRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>DTO книги</returns>
    /// <exception cref="KeyNotFoundException">Если книга не найдена</exception>
    public async Task<BookDto> Get(Guid id, CancellationToken ct = default)
    {
        var entity = await _repository.Get(id, ct)
            ?? throw new KeyNotFoundException($"Book with ID {id} not found");
        return _mapper.Map<BookDto>(entity);
    }

    /// <summary>
    /// Получает все книги
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список DTO книг</returns>
    public async Task<IReadOnlyList<BookDto>> GetAll(CancellationToken ct = default)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<BookDto>>(entities);
    }

    /// <summary>
    /// Создает новую книгу
    /// </summary>
    /// <param name="dto">DTO книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный DTO книги</returns>
    public async Task<BookDto> Create(BookDto dto, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Book>(dto);
        var created = await _repository.Create(entity, ct);
        return _mapper.Map<BookDto>(created);
    }

    /// <summary>
    /// Обновляет существующую книгу
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный DTO книги</returns>
    /// <exception cref="KeyNotFoundException">Если книга не найдена</exception>
    public async Task<BookDto> Update(BookDto dto, Guid dtoId, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Book>(dto);
        entity.Id = dtoId;

        var updated = await _repository.Update(entity, ct);

        return updated == null
            ? throw new KeyNotFoundException($"Book with ID {dtoId} not found")
            : _mapper.Map<BookDto>(updated);
    }

    /// <summary>
    /// Удаляет книгу по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public async Task<bool> Delete(Guid dtoId, CancellationToken ct = default)
    {
        return await _repository.Delete(dtoId, ct);
    }
}