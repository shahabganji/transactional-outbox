using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Infrastructure.Postgres;
using MassTransit.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.TransactionalOutbox.Application.HostedServices;

public sealed class DatabaseMigratorHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigratorHostedService(IServiceProvider scopeServiceProvider)
    {
        _serviceProvider = Guard.Against.Null(scopeServiceProvider);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var orderContext =
            _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<OrderContext>();
        await orderContext.Database.MigrateAsync(cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
