using Demo.TransactionalOutbox.Contracts.OrderAggregate.Commands;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;
using MediatR;

namespace Demo.TransactionalOutbox.Application.Handlers;

public sealed class CancelOrderConsumer : IRequestHandler<CancelOrder, CancelOrderResult>
{
    public Task<CancelOrderResult> Handle(CancelOrder request, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
