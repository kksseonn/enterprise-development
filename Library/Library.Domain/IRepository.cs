using System.Linq.Expressions;

namespace Library.Domain;

/// <summary>
/// Базовый интерфейс репозитория для CRUD-операций
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Создает новую сущность
    /// </summary>
    /// <param name="entity">Объект сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная сущность</returns>
    public Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все сущности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список всех сущностей</returns>
    public Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    /// <param name="entity">Объект сущности с обновленными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная сущность или null, если не найдена</returns>
    public Task<TEntity?> Update(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>true, если удаление прошло успешно, иначе false</returns>
    public Task<bool> Delete(Guid entityId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает сущность по идентификатору, опционально включая связанные сущности/коллекции
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <param name="includes">Массив выражений для включения связанных свойств</param>
    /// <returns>Сущность или null, если не найдена</returns>
    public Task<TEntity?> Get(
        Guid entityId,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes
    );
}