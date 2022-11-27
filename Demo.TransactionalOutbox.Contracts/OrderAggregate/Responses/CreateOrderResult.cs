namespace Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;

public record CreateOrderResult
{
    public required Guid OrderId { get; init; }
}
