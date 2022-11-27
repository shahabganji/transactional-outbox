using MediatR;

namespace Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;

public class CancelOrderResult : IRequest<Unit>
{
}
