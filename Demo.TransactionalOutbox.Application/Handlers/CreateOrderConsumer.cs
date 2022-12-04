using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Commands;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Responses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using MediatR;

namespace Demo.TransactionalOutbox.Application.Handlers;

public sealed class CreateOrderConsumer : IRequestHandler<CreateOrder, CreateOrderResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Order> _repository;

    public CreateOrderConsumer(
        IRepository<Order> repository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork);
        _repository = Guard.Against.Null(repository);
    }


    public async Task<CreateOrderResult> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = new Order(request.ProductId, request.Quantity);
        
        await _repository.StoreAsync(order, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new CreateOrderResult { OrderId = order.Id };
    }
}
