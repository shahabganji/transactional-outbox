namespace Demo.TransactionalOutbox.Domain.Abstractions;

public interface IEventEmitter
{
    public Task Emit(IEnumerable<IDomainEvent> domainEvents);
}
