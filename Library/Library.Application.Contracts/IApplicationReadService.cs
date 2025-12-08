namespace Library.Application.Contracts;

/// <summary>
/// Интерфейс для сервисов чтения данных приложения
/// </summary>
/// <typeparam name="TDto">Тип DTO</typeparam>
/// <typeparam name="TKey">Тип ключа</typeparam>
public interface IApplicationReadService<TDto, TKey>
    where TDto : class
    where TKey : struct
{
    /// <summary>
    /// Получает DTO по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Объект DTO</returns>
    public Task<TDto> Get(TKey dtoId, CancellationToken ct = default);

    /// <summary>
    /// Получает все DTO
    /// </summary>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список всех DTO</returns>
    public Task<IReadOnlyList<TDto>> GetAll(CancellationToken ct = default);
}