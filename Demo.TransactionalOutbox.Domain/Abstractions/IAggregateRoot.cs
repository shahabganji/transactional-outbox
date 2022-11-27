namespace Demo.TransactionalOutbox.Domain.Abstractions;

public interface IAggregateRoot
{
    Guid Id { get; }
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
