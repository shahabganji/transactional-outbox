namespace Demo.TransactionalOutbox.Domain.Abstractions;

public class AggregateRoot : IAggregateRoot
{
    public Guid Id { get; private set; }
    
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(): this(Guid.NewGuid())
    {
    }

    protected AggregateRoot(Guid id) => Id = id;

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
