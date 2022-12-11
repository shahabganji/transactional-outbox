using System.Data;
using Demo.TransactionalOutbox;
using Demo.TransactionalOutbox.MessageRelay;
using Npgsql;
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


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddSingleton<IDbConnection>(_ =>
            new NpgsqlConnection(host.Configuration.GetConnectionString("Database")));
        services.AddEventEmitter(host.Configuration);
        services.AddHostedService<BackgroundEventEmitter>();
    })
    .UseSerilog()
    .Build();

host.Run();
