using System.Threading;
using System.Threading.Tasks;

namespace Library.Generator.Nats.Host.Services;

public interface IBorrowsGenerator
{
    public Task GenerateAsync(
        int batchSize,
        int batchesCount,
        CancellationToken cancellationToken = default);
}
