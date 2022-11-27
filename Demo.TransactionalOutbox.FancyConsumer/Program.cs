using Demo.TransactionalOutbox.FancyConsumer;
using MassTransit;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host,services) =>
    {
        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(host.Configuration.GetConnectionString("MessageBroker"));
                configurator.ConfigureEndpoints(context);
            });
            mt.AddConsumer<OrderCreatedConsumer>();
        });
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
