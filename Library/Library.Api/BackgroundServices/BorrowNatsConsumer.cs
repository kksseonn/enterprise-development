using Library.Application.Contracts.Borrow;
using Library.Infrastructure.Nats;
using Library.Infrastructure.Nats.Options;
using Microsoft.Extensions.Options;
using NATS.Client.Core;

namespace Library.Api.Host.BackgroundServices;

public class BorrowNatsConsumer(
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