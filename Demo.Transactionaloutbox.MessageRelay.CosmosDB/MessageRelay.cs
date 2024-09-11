using System.Text.Json.Nodes;
using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.Transactionaloutbox.MessageRelay.CosmosDB.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Demo.Transactionaloutbox.MessageRelay.CosmosDB;

public class MessageRelay
{
    private readonly IEventEmitter _eventEmitter;
    private readonly ILogger<MessageRelay> _logger;

    public MessageRelay(
        IEventEmitter eventEmitter,
        ILogger<MessageRelay> logger)
    {
        _eventEmitter = eventEmitter;
        _logger = logger;
    }

    [Function("MessageRelay")]
    public async Task Run([CosmosDBTrigger(
            databaseName: "WebShop",
            containerName: "Orders",
            Connection = "Database",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)]
        IReadOnlyList<JsonObject> inputs)
    {
        Guard.Against.Null(inputs);

        foreach (var input in inputs)
        {
            if (!input.TryGetData(out var data))
                continue;

            await _eventEmitter.Emit([data]);

            _logger.LogInformation("The message published to the message broker: {Data}", data);
        }
    }
}

