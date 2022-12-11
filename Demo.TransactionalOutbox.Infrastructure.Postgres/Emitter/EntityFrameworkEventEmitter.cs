using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres.Emitter;

public sealed class EntityFrameworkEventEmitter : IEventEmitter
{
    private readonly OrderContext _orderContext;

    public EntityFrameworkEventEmitter(OrderContext orderContext)
    {
        _orderContext = Guard.Against.Null(orderContext);
    }
    
    public Task Emit(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var underlyingObject = (object)domainEvent;
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(underlyingObject);
            _orderContext.Outboxes.Add(new Outbox(jsonContent, domainEvent.GetType().FullName!));
        }
        
        return Task.CompletedTask;
    }
}
