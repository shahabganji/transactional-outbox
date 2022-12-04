using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Infrastructure.Postgres.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres;

public sealed class OrderContext : DbContext, IUnitOfWork
{
    private readonly IEventEmitter _eventEmitter;
    private readonly IList<IDomainEvent> _domainEvents;

    public OrderContext(
        DbContextOptions<OrderContext> options,
        IEventEmitter eventEmitter
    ) : base(options)
    {
        _eventEmitter = Guard.Against.Null(eventEmitter);
        
        _domainEvents = new List<IDomainEvent>();
    }

    internal DbSet<OrderEntity> Orders { get; private set; } = default!;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _eventEmitter.Emit(_domainEvents);
        await this.SaveChangesAsync(cancellationToken);

        _domainEvents.Clear();
    }

    public void AddDomainEventsForAggregate(IAggregateRoot aggregateRoot)
    {
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            _domainEvents.Add(domainEvent);
        }
        aggregateRoot.ClearDomainEvents();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
    }
}
