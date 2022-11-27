using Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;
using MediatR;

namespace Demo.TransactionalOutbox.Contracts.OrderAggregate.Commands;

public sealed class CreateOrder : IRequest<CreateOrderResult>
{
    public int Quantity { get; init; }
    public Guid ProductId { get; init; }
}

public sealed class CancelOrder: IRequest<CancelOrderResult>
{
    public Guid OrderId { get; set; }
}
