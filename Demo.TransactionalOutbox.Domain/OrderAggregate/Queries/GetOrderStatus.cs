namespace Demo.TransactionalOutbox.Domain.OrderAggregate.Queries;

public sealed class GetOrderStatus
{
    public Guid OrderId { get; set; }
}
