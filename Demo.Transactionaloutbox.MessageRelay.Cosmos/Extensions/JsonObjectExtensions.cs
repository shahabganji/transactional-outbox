using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Domain.Events;

namespace Demo.Transactionaloutbox.MessageRelay.Cosmos.Extensions;

internal static class JsonObjectExtensions
{
    internal static bool TryGetData(this JsonObject input,
        [NotNullWhen(true)] out IDomainEvent data)
    {
        data = null;

        if (!input.TryGetPropertyValue("Payload", out var payloadNode))
            return false;

        if (!input.TryGetPropertyValue("Type", out var typeNode))
            return false;

        if (!input.TryGetPropertyValue("id", out var idNode))
            return false;

        var id = idNode!.ToString();
        if (!id.StartsWith(nameof(IDomainEvent)))
            return false;

        var typeName = typeNode!.ToString();
        var type = typeof(OrderCreated).Assembly.GetType(typeName);
        if (type is null)
            return false;

        var payload = payloadNode!.ToJsonString();
        if (string.IsNullOrWhiteSpace(payload))
            return false;

        data = (IDomainEvent)JsonSerializer.Deserialize(payload, type, new JsonSerializerOptions());

        return data is not null;
    }
}
