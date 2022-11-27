using Demo.TransactionalOutbox.Contracts.OrderAggregate.Queries;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using MediatR;

namespace Demo.TransactionalOutbox.Application.Handlers;

public sealed class GetOrderConsumer
    : IRequestHandler<GetOrderStatus, GetOrderStatusResult>
{
    public Task<GetOrderStatusResult> Handle(GetOrderStatus request, CancellationToken cancellationToken)
        => Task.FromResult(new GetOrderStatusResult()
        {
            Status = OrderStatus.Accepted
        });
}
