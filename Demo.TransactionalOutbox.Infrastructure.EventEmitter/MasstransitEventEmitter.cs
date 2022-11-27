using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using MassTransit;

namespace Demo.TransactionalOutbox.Infrastructure.EventEmitter;

public sealed class MasstransitEventEmitter : IEventEmitter
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MasstransitEventEmitter(IPublishEndpoint publishEndpoint)
    {
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
        catch (Exception)
        {
            // ignored
        }
    }
}
