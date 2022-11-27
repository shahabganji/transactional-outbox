using Demo.TransactionalOutbox.Application.Handlers;
using Demo.TransactionalOutbox.Application.HostedServices;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Infrastructure.Postgres;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.TransactionalOutbox.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddHostedService<DatabaseMigratorHostedService>();
        services.AddMediatR(typeof(CreateOrderConsumer).Assembly);

        services.AddRepositories(configuration)
            .AddEventEmitter(configuration);
        
        return services;
    }
}
