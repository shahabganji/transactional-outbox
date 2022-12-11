using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.OrderAggregate;
using Demo.TransactionalOutbox.Infrastructure.Postgres;
using Demo.TransactionalOutbox.Infrastructure.Postgres.Emitter;
using Demo.TransactionalOutbox.Infrastructure.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Demo.TransactionalOutbox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(
            builder => builder.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<OrderContext>());

        return services;
    }
    
    public static IServiceCollection AddEventEmitter(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventEmitter, EntityFrameworkEventEmitter>();

        return services;
    }
}
