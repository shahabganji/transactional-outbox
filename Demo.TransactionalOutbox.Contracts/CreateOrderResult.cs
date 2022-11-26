namespace Demo.TransactionalOutbox.Contracts;

public interface CreateOrderResult
{
    public Guid OrderId { get; set; }
}
