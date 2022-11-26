using Demo.TransactionalOutbox.Api.DataTransferObjects;
using Demo.TransactionalOutbox.Contracts;
using Demo.TransactionalOutbox.Domain.OrderAggregate.Commands;
using Demo.TransactionalOutbox.Domain.OrderAggregate.Queries;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Demo.TransactionalOutbox.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CreateOrderDto createOrderDto,
        [FromServices] IRequestClient<CreateOrder> createOrderRequestClient)
    {
        var result = await createOrderRequestClient.GetResponse<CreateOrderResult>(
            new CreateOrder{ ProductId = createOrderDto.ProductId,Quantity= createOrderDto.Quantity });
        return Ok(result);
    }

    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid orderId,
        [FromServices] IRequestClient<CancelOrder> cancelOrderRequestClient)
    {
        var result =
            await cancelOrderRequestClient.GetResponse<CancelOrderResult>(new CancelOrder { OrderId = orderId });
        return Ok(result);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid orderId,
        [FromServices] IRequestClient<GetOrderStatus> getOrderRequestClient)
    {
        var result = await getOrderRequestClient
            .GetResponse<GetOrderStatusResult>(new GetOrderStatus { OrderId = orderId });
        return Ok(new GetOrderStatusDto { Status = result.Message.Status });
    }
}
