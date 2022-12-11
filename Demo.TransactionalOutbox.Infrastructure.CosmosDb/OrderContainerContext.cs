using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Infrastructure.CosmosDb.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Demo.TransactionalOutbox.Infrastructure.CosmosDb;

public sealed class OrderContainerContext : IUnitOfWork
{
    private readonly ILogger<OrderContainerContext> _logger;
    public Container AzureCosmosOrderContainer { get; }

    private readonly List<CosmosDbOrder> _orders = new();
    private readonly List<CosmoOrderOutbox> _events = new();

    public OrderContainerContext(Container azureCosmosOrderContainer, ILogger<OrderContainerContext> logger)
    {
        _logger = Guard.Against.Null(logger);
        AzureCosmosOrderContainer = Guard.Against.Null(azureCosmosOrderContainer);
    }

    internal void AddOrder(CosmosDbOrder order)
    {
        _orders.Add(order);
    }

    internal void AddEvents(params IDomainEvent[] domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            _events.Add(new CosmoOrderOutbox(domainEvent.GetType().FullName!, domainEvent));
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var partitionKeyValue = _orders.Single().OrderId;
        var tr = AzureCosmosOrderContainer.CreateTransactionalBatch(new PartitionKey(partitionKeyValue));

        foreach (var cosmosDbOrder in _orders)
            tr.UpsertItem(cosmosDbOrder);

        foreach (var domainEvent in _events)
        {
            domainEvent.SetOrderId(partitionKeyValue);
            tr.CreateItem(domainEvent);
        }

        var transactionResult = await tr.ExecuteAsync(cancellationToken);
        _logger.LogInformation("Request Charge: {RequestCharge}" , transactionResult.RequestCharge);
    }
}
