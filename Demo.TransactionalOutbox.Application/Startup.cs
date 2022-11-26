using Demo.TransactionalOutbox.Application.BackgroundServices;
using Demo.TransactionalOutbox.Application.Consumers;
using MassTransit;

namespace Demo.TransactionalOutbox.Application;

public static class Startup
{
    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddHostedService<DatabaseMigratorBackgroundService>();
        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(host.Configuration.GetConnectionString("MessageBroker"));
                configurator.ConfigureEndpoints(context);
            });

            mt.AddConsumersFromNamespaceContaining<CreateOrderConsumer>();
        });

        services.AddRepositories(host.Configuration);
    }
}
