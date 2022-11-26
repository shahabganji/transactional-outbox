namespace Demo.TransactionalOutbox.Domain.Abstractions;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T?> GetAsync(Guid key, CancellationToken cancellationToken = default);
    Task StoreAsync(T aggregateRoot, CancellationToken cancellationToken = default);
}
