using Demo.TransactionalOutbox.Domain.Events;
using MassTransit;

namespace Demo.TransactionalOutbox.FancyConsumer;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        _logger.LogInformation("Order created: {@Order}" , context.Message);
        return Task.CompletedTask;
    }
}
