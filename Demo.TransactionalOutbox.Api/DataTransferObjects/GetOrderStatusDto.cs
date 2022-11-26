using Demo.TransactionalOutbox.Domain.OrderAggregate;

namespace Demo.TransactionalOutbox.Api.DataTransferObjects;

public sealed class GetOrderStatusDto
{
    public OrderStatus Status { get; init; }
}
