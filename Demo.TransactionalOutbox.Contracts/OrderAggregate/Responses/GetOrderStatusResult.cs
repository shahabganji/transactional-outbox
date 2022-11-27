using Demo.TransactionalOutbox.Domain.OrderAggregate;

namespace Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;

public class GetOrderStatusResult
{
    public OrderStatus Status { get; set; }
}
