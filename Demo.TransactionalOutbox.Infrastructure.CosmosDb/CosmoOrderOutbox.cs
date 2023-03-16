using System.Text;
using Demo.TransactionalOutbox.Domain.Abstractions;

namespace Demo.TransactionalOutbox.Infrastructure.CosmosDb;

public sealed class CosmoOrderOutbox
{
    public string id { get; set; } = default!;
    public string OrderId { get; set; } = default!;
    public string Type { get; init; }
    public object Payload { get; init; }

    internal void SetOrderId(string orderId)
    {
        id = $"{nameof(IDomainEvent)}-{orderId}-{Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))}";
        OrderId = orderId;
    }
    internal CosmoOrderOutbox(string type, IDomainEvent payload)
    {
        Type = type;
        Payload = payload;
    }
}
