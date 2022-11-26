namespace Demo.TransactionalOutbox.Domain.Abstractions;

public interface IDomainEvent
{
    public DateTimeOffset OccuredAt { get;}
}
