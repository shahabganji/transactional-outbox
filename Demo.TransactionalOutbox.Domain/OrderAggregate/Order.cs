using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.Events;

namespace Demo.TransactionalOutbox.Domain.OrderAggregate;

public class Order : AggregateRoot
{
    public int Amount { get; init; }
    public Guid ProductId { get; init; }
    public OrderStatus Status { get; private set; }

    public Order(Guid productId, int amount)
    {
        Amount = amount;
        ProductId = productId;
        Status = OrderStatus.Requested;
        RaiseDomainEvent(new OrderCreated(DateTimeOffset.Now, Id, ProductId, Amount));
    }

    public void Cancel()
    {
        Status = OrderStatus.Canceled;
        RaiseDomainEvent(new OrderCanceled(DateTimeOffset.Now, Id));
    }
    
    public void Accept()
    {
        Status = OrderStatus.Accepted;
        RaiseDomainEvent(new OrderAccepted(DateTimeOffset.Now, Id, ProductId, Amount));
    }
}
