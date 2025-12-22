using Library.Application.Contracts.Borrow;
using Library.Infrastructure.Nats.Options;
using Microsoft.Extensions.Options;
using NATS.Client.Core;

namespace Library.Api.Host.BackgroundServices;

/// <summary>
/// Фоновый сервис для чтения сообщений Borrow из NATS JetStream и сохранения в базу данных
/// </summary>
public sealed class BorrowNatsConsumer(
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    ILogger<BorrowNatsConsumer> logger,
    IOptions<NatsOptions> options)
    : LibraryJetStreamConsumer<BorrowDto, BorrowCrudDto, Guid>(
        connection,
        scopeFactory,
        logger,
        options,
        consumerName: "api-borrow-consumer")
{
}