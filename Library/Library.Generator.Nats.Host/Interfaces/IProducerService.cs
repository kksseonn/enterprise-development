namespace Library.Generator.Nats.Host.Interfaces;

/// <summary>
/// Интерфейс сервиса для публикации батчей данных в NATS
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Публикует батч данных в NATS
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <param name="batch">Батч данных для публикации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача выполнения операции</returns>
    public Task PublishBatchAsync<T>(IReadOnlyCollection<T> batch, CancellationToken cancellationToken = default);
}