using System.Data;
using System.Text.Json;
using Ardalis.GuardClauses;
using Dapper;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.Events;

namespace Demo.TransactionalOutbox.MessageRelay;

public class BackgroundEventEmitter : BackgroundService
{
    private readonly ILogger<BackgroundEventEmitter> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbConnection _connection;
    private readonly PeriodicTimer _periodicTimer;

    public BackgroundEventEmitter(IDbConnection connection, ILogger<BackgroundEventEmitter> _logger,
        IServiceProvider serviceProvider)
    {
        this._logger = Guard.Against.Null(_logger);
        _serviceProvider = Guard.Against.Null(serviceProvider);
        _connection = Guard.Against.Null(connection);
        _periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _periodicTimer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            await PublishMessages();
        }
    }

    private async Task PublishMessages()
    {
        var eventEmitter = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IEventEmitter>();

        try
        {
            var outboxMessages = await _connection.QueryAsync<OutboxMessages>(
                """
                    Select o."Id", o."Payload", o."Type"
                    from "outbox" o
                    where o."IsPublished" = false
                    order by o."CreatedAt"
                    offset 0 limit 10
                """);
            foreach (var message in outboxMessages)
            {
                var payloadType = typeof(OrderCreated).Assembly.DefinedTypes.Single(t => t.FullName == message.Type);
                var payload = JsonSerializer.Deserialize(message.Payload, payloadType)!;
                
                await eventEmitter.Emit(new[] { payload as IDomainEvent, }!);
                _logger.LogInformation("Message published from Outbox: {@Event}", message);
                
                await _connection.ExecuteAsync("""
                update "outbox"
                    set
                        "IsPublished" = true, 
                        "PublishedAt" = @now
                where "Id" = @id
            """, new { now = DateTimeOffset.UtcNow, id = message.Id });
                _logger.LogInformation("Message {MessageId} marked as published in the Outbox", message.Id);
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "An exception occured when trying to publish the event form outbox");
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _periodicTimer.Dispose();
        return base.StopAsync(cancellationToken);
    }
}

public sealed class OutboxMessages
{
    public Guid Id { get; init; }
    public string Payload { get; init; } = default!;
    public string Type { get; init; } = default!;
}
