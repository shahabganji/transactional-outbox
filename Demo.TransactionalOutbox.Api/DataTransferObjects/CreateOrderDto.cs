namespace Demo.TransactionalOutbox.Api.DataTransferObjects;

public sealed record CreateOrderDto(Guid ProductId, int Quantity );
