using Demo.TransactionalOutbox.Domain.Abstractions;

namespace Demo.TransactionalOutbox.Domain.Events;

public sealed record OrderCanceled(DateTimeOffset OccuredAt, Guid OrderId) : IDomainEvent;
