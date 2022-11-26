using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Demo.TransactionalOutbox.Application.BackgroundServices;

public sealed class DatabaseMigratorBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigratorBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = Guard.Against.Null(serviceProvider, nameof(serviceProvider));
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var orderContext = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<OrderContext>();
        await orderContext.Database.MigrateAsync(cancellationToken: stoppingToken);
    }
}
