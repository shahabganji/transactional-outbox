using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Contracts;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using Demo.TransactionalOutbox.Domain.OrderAggregate.Commands;
using MassTransit;

namespace Demo.TransactionalOutbox.Application.Consumers;

public sealed class CreateOrderConsumer
    : IConsumer<CreateOrder>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Order> _repository;
    private readonly ILogger<CreateOrderConsumer> _logger;

    public CreateOrderConsumer(
        IRepository<Order> repository,
        IUnitOfWork unitOfWork,
        ILogger<CreateOrderConsumer> logger)
    {
        _logger = Guard.Against.Null(logger);
        _unitOfWork = Guard.Against.Null(unitOfWork);
        _repository = Guard.Against.Null(repository);
    }

    public async Task Consume(ConsumeContext<CreateOrder> context)
    {
        var order = new Order(Guid.NewGuid(), 1);
        await _repository.StoreAsync(order);
        await _unitOfWork.CommitAsync();
        await context.RespondAsync<CreateOrderResult>(new { OrderId = order.Id });
    }
}
