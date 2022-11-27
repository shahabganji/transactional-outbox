using Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;
using MediatR;

namespace Demo.TransactionalOutbox.Contracts.OrderAggregate.Queries;

public sealed class GetOrderStatus : IRequest<GetOrderStatusResult>
{
    public Guid OrderId { get; set; }
}
