namespace Demo.TransactionalOutbox.Domain.Abstractions;

public interface IAggregateRoot
{
    Guid Id { get; }
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
