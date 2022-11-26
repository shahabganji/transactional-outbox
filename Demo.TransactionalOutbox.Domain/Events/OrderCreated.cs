using Demo.TransactionalOutbox.Domain.Abstractions;

namespace Demo.TransactionalOutbox.Domain.Events;

public sealed record OrderCreated(
    DateTimeOffset OccuredAt,
    Guid OrderId, Guid ProductId, int Amount) : IDomainEvent;
