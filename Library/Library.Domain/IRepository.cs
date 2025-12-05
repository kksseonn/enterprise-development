using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Domain;

/// <summary>
/// Базовый интерфейс репозитория для CRUD-операций
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Создать новую сущность
    /// </summary>
    public Task<TEntity> Create(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    public Task<TEntity?> Get(
        Guid entityId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все сущности
    /// </summary>
    public Task<IReadOnlyList<TEntity>> GetAll(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить сущность
    /// </summary>
    public Task<TEntity?> Update(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    public Task<bool> Delete(
        Guid entityId,
        CancellationToken cancellationToken = default);
}
