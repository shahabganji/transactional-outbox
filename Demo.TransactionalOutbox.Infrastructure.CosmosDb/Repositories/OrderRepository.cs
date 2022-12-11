using System.Net;
using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using Demo.TransactionalOutbox.Infrastructure.CosmosDb.Entities;
using Microsoft.Azure.Cosmos;

namespace Demo.TransactionalOutbox.Infrastructure.CosmosDb.Repositories;

public sealed class OrderRepository : IRepository<Order>
{
    private readonly OrderContainerContext _containerContext;

    public OrderRepository(OrderContainerContext containerContext)
    {
        _containerContext = Guard.Against.Null(containerContext);
    }

    public async Task<Order?> GetAsync(Guid key, CancellationToken cancellationToken = default)
    {
        var orderEntity = await _containerContext.AzureCosmosOrderContainer.ReadItemAsync<CosmosDbOrder>(
            key.ToString(),
            new PartitionKey(key.ToString()),
            cancellationToken: cancellationToken);

        var order = new Order(orderEntity.Resource.ProductId, orderEntity.Resource.Amount,
            Guid.Parse(orderEntity.Resource.OrderId));
        order.ClearDomainEvents();

        return orderEntity.StatusCode == HttpStatusCode.OK
            ? order
            : null;
    }

    public Task StoreAsync(Order aggregateRoot, CancellationToken cancellationToken = default)
    {
        var cosmosEntity = (CosmosDbOrder)aggregateRoot;
        _containerContext.AddOrder(cosmosEntity);
        return Task.CompletedTask;
    }
}
