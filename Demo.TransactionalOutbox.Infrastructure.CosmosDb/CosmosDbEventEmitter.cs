using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;

namespace Demo.TransactionalOutbox.Infrastructure.CosmosDb;

public sealed class CosmosDbEventEmitter : IEventEmitter
{
    private readonly OrderContainerContext _containerContext;

    public CosmosDbEventEmitter(OrderContainerContext containerContext)
    {
        _containerContext = Guard.Against.Null(containerContext);
    }
    public Task Emit(IEnumerable<IDomainEvent> domainEvents)
    {
        _containerContext.AddEvents(domainEvents.ToArray());
        return Task.CompletedTask;
    }
}
