using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Infrastructure.Postgres.Configurations;
using Demo.TransactionalOutbox.Infrastructure.Postgres.Emitter;
using Microsoft.EntityFrameworkCore;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres;

public sealed class OrderContext : DbContext, IUnitOfWork
{
    public OrderContext(
        DbContextOptions<OrderContext> options
    ) : base(options)
    {
    }

    internal DbSet<OrderEntity> Orders { get; private set; } = default!;
    internal DbSet<Outbox> Outboxes { get; private set; } = default!;

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await this.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxConfiguration());
    }
}
