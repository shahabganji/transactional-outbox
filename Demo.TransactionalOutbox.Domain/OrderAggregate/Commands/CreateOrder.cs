namespace Demo.TransactionalOutbox.Domain.OrderAggregate.Commands;

public sealed class CreateOrder
{
    public int Quantity { get; init; }
    public Guid ProductId { get; init; }
}

public sealed class CancelOrder
{
    public Guid OrderId { get; set; }
}

public sealed class AcceptOrder
{
    public Guid OrderId { get; set; }
}
