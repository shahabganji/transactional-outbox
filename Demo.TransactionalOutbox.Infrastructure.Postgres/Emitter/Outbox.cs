using System.Diagnostics.CodeAnalysis;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres.Emitter;

public class Outbox
{
    public Outbox(
        [StringSyntax(StringSyntaxAttribute.Json)]
        string payload,
        string type)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
        PublishedAt = null;
        IsPublished = false;
        
        Payload = payload;
        Type = type;
    }

    public Guid Id { get; init; }
    public bool IsPublished { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? PublishedAt { get; init; }
    public string Payload { get; init; }
    public string Type { get; init; }
}
