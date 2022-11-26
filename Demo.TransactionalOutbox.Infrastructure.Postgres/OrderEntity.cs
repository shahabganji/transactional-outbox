using Demo.TransactionalOutbox.Domain.OrderAggregate;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres;

internal class OrderEntity
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }

    public static OrderEntity From(Order order)
        => new()
        {
            Id = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Amount
        };
}
