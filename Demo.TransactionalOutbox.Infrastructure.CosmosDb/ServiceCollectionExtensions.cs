using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using Demo.TransactionalOutbox.Infrastructure.CosmosDb;
using Demo.TransactionalOutbox.Infrastructure.CosmosDb.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Demo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<OrderContainerContext>(sp =>
        {
            var connectionString = configuration.GetConnectionString("Database");
            var orderContainer = new CosmosClient(connectionString);
            var collection = orderContainer.GetContainer("ms_talk", "orders");
            return new OrderContainerContext(collection, sp.GetRequiredService<ILogger<OrderContainerContext>>());
        });
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<OrderContainerContext>());
        services.AddScoped<IRepository<Order>, OrderRepository>();
        return services;
    }

    public static IServiceCollection AddEventEmitter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventEmitter, CosmosDbEventEmitter>();
        return services;
    }
}
