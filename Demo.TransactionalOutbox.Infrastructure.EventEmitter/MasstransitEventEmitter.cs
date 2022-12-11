using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Demo.TransactionalOutbox.Infrastructure.EventEmitter;

public sealed class MasstransitEventEmitter : IEventEmitter
{
    private readonly ILogger<MasstransitEventEmitter> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public MasstransitEventEmitter(IPublishEndpoint publishEndpoint, ILogger<MasstransitEventEmitter> logger)
    {
        _logger = Guard.Against.Null(logger);
        _publishEndpoint = Guard.Against.Null(publishEndpoint);
    }
    
    public async Task Emit(IEnumerable<IDomainEvent> domainEvents)
    {
        try
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publishEndpoint.Publish(domainEvent, domainEvent.GetType(), CancellationToken.None);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Publish failed");
            throw;
        }
    }
}
