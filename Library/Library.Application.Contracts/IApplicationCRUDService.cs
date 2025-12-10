using Library.Application.Contracts;

namespace Library.Application.Contracts;

/// <summary>
/// Интерфейс для сервисов CRUD операций приложения
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения (Read)</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления (Create/Update)</typeparam>
/// <typeparam name="TKey">Тип ключа (идентификатора)</typeparam>
public interface IApplicationCrudService<TDto, TCreateUpdateDto, TKey>
    : IApplicationReadService<TDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создает новую сущность
    /// </summary>
    /// <param name="dto">DTO для создания</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Созданный объект DTO</returns>
    public Task<TDto> Create(TCreateUpdateDto dto, CancellationToken ct = default);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <param name="dtoId">Идентификатор сущности</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Обновленный объект DTO</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId, CancellationToken ct = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор сущности</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns><see langword="true"/>, если удаление прошло успешно, иначе <see langword="false"/></returns>
    public Task<bool> Delete(TKey dtoId, CancellationToken ct = default);
}