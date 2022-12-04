using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres.Repositories;

public sealed class OrderRepository : IRepository<Order>
{
    private readonly OrderContext _context;

    public OrderRepository(OrderContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public async Task<Order?> GetAsync(Guid key, CancellationToken cancellationToken = default)
    {
        var orderEntity = await _context.Orders.FindAsync(key, cancellationToken);
        if (orderEntity is null)
            return null;

        var order = new Order(orderEntity.ProductId, orderEntity.Quantity);
        order.ClearDomainEvents();
        return order;
    }

    public async Task StoreAsync(Order aggregateRoot, CancellationToken cancellationToken = default)
    {
        var orderEntity = OrderEntity.From(aggregateRoot);
        
        var alreadyExists = await _context.Orders.FindAsync(aggregateRoot.Id, cancellationToken);
        
        _context.AddDomainEventsForAggregate(aggregateRoot);
        _context.Entry(orderEntity).State = alreadyExists is null
            ? EntityState.Added
            : EntityState.Modified;
    }
}
