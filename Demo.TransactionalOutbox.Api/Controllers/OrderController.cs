using Ardalis.GuardClauses;
using Demo.TransactionalOutbox.Api.DataTransferObjects;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Commands;
using Demo.TransactionalOutbox.Contracts.OrderAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.TransactionalOutbox.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = Guard.Against.Null(mediator);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CreateOrderDto createOrderDto)
    {
        var result = await _mediator.Send(
            new CreateOrder { ProductId = createOrderDto.ProductId, Quantity = createOrderDto.Quantity });
        return Ok(result);
    }

    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid orderId)
    {
        var result = await _mediator.Send(new CancelOrder { OrderId = orderId });
        return Ok(result);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid orderId)
    {
        var result = await _mediator.Send(new GetOrderStatus { OrderId = orderId });
        return Ok(new GetOrderStatusDto { Status = result.Status });
    }
}
