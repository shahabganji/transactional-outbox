using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.MessageRelay.CosmosDB.Extensions;
using JetBrains.Annotations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Demo.TransactionalOutbox.MessageRelay.CosmosDB;

public class MessageRelayFunction
{
    private readonly IEventEmitter _eventEmitter;
    private readonly ILogger<MessageRelayFunction> _logger;

    public MessageRelayFunction(
        [NotNull] IEventEmitter eventEmitter,
        [NotNull] ILogger<MessageRelayFunction> logger)
    {
        _logger = logger;
        _eventEmitter = eventEmitter;
    }

    [Function("MessageRelayFunction")]
    public async Task Run([CosmosDBTrigger(
            databaseName: "ms_talk",
            collectionName: "Orders",
            ConnectionStringSetting = "Database",
            LeaseCollectionName = "leases")]
        IReadOnlyList<JsonObject> inputs, FunctionContext context)
    {
        Guard.Against.Null(inputs);

        foreach (var input in inputs)
        {
            if (!input.TryGetData(out var data))
                continue;

            await _eventEmitter.Emit(new[] { data });

            _logger.LogInformation("The message published to the message broker: {Data}", data);
        }
    }
}
