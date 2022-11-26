namespace Demo.TransactionalOutbox.Domain.OrderAggregate;

public enum OrderStatus
{
    Requested = 0,
    Accepted = 1,
    Canceled = 2
}
