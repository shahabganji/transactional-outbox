using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Commands;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using MediatR;

namespace Demo.TransactionalOutbox.Application.Handlers;

public sealed class CreateOrderConsumer : IRequestHandler<CreateOrder, CreateOrderResult>
{
    private readonly IEventEmitter _eventEmitter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Order> _repository;

    public CreateOrderConsumer(
        IRepository<Order> repository,
        IUnitOfWork unitOfWork,
        IEventEmitter eventEmitter)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork);
        _repository = Guard.Against.Null(repository);
        _eventEmitter = Guard.Against.Null(eventEmitter);
    }


    public async Task<CreateOrderResult> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = new Order(request.ProductId, request.Quantity);
        
        await _repository.StoreAsync(order, cancellationToken);
        await _eventEmitter.Emit(order.DomainEvents);
        await _unitOfWork.CommitAsync(cancellationToken);

        order.ClearDomainEvents();
        return new CreateOrderResult { OrderId = order.Id };
    }
}
