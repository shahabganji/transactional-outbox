using Demo.TransactionalOutbox.Contracts;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using Demo.TransactionalOutbox.Domain.OrderAggregate.Queries;
using MassTransit;

namespace Demo.TransactionalOutbox.Application.Consumers;

public sealed class GetOrderConsumer
    : IConsumer<GetOrderStatus>
{
    public async Task Consume(ConsumeContext<GetOrderStatus> context)
        => await context.RespondAsync(new GetOrderStatusResult()
        {
            Status = OrderStatus.Accepted
        });
}
