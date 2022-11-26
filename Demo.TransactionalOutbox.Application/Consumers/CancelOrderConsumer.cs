using Demo.TransactionalOutbox.Domain.OrderAggregate.Commands;
using MassTransit;

namespace Demo.TransactionalOutbox.Application.Consumers;

public sealed class CancelOrderConsumer 
    : IConsumer<CancelOrder>
{
    public Task Consume(ConsumeContext<CancelOrder> context) => throw new NotImplementedException();
}