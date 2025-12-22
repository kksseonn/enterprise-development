namespace Library.Generator.Nats.Host.Services;

/// <summary>
/// Интерфейс сервиса генерации тестовых карточек Borrow
/// </summary>
public interface IBorrowsGenerator
{
    /// <summary>
    /// Генерирует заданное количество батчей Borrow и публикует их
    /// </summary>
    /// <param name="batchSize">Количество элементов в одном батче</param>
    /// <param name="batchesCount">Количество батчей</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача выполнения операции</returns>
    public Task GenerateAsync(int batchSize, int batchesCount, CancellationToken cancellationToken = default);
}