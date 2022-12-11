using Ardalis.GuardClauses;
using Microsoft.Extensions.Hosting;

namespace Demo.TransactionalOutbox.Application.HostedServices;

public sealed class DatabaseMigratorHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigratorHostedService(IServiceProvider scopeServiceProvider)
    {
        _serviceProvider = Guard.Against.Null(scopeServiceProvider);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
