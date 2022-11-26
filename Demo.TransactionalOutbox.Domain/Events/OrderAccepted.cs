using Demo.TransactionalOutbox.Domain.Abstractions;

namespace Demo.TransactionalOutbox.Domain.Events;

public sealed record OrderAccepted(DateTimeOffset OccuredAt, Guid Id, Guid ProductId, int Amount) : IDomainEvent;
