using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.OrderAggregate;

namespace Demo.TransactionalOutbox.Infrastructure.CosmosDb.Entities;

internal sealed class CosmosDbOrder
{
    // ReSharper disable once InconsistentNaming
    public string id { get; init; }
    public string OrderId { get; init; }
    
    public int Amount { get; init; }
    public Guid ProductId { get; init; }
    public OrderStatus Status { get; init; }

    private CosmosDbOrder(string id)
    {
        this.id = id;
        OrderId = id;
    }

    public static explicit operator CosmosDbOrder(Order order)
    {
        Guard.Against.Null(order);

        var orderId = order.Id.ToString();

        return new CosmosDbOrder(orderId)
        {
            Amount = order.Amount, ProductId = order.ProductId, Status = order.Status
        };
    }
}
