namespace Demo.TransactionalOutbox.Application.Configurations;

public sealed class MessageBrokerConfiguration
{
    public string CommandBus { get; set; }
    public string EventBus { get; set; }
}
