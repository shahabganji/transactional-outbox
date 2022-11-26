using Demo.TransactionalOutbox.Domain.OrderAggregate;

namespace Demo.TransactionalOutbox.Contracts;

public class GetOrderStatusResult
{
    public OrderStatus Status { get; set; }
}
