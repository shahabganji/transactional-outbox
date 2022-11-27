using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Infrastructure.EventEmitter;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Demo.TransactionalOutbox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventEmitter(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(configuration.GetConnectionString("MessageBroker"));
                configurator.ConfigureEndpoints(context);
            });
        });
        
        services.AddScoped<IEventEmitter, MasstransitEventEmitter>();

        return services;
    }
}
