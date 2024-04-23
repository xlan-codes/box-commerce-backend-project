using Application.UseCases.PlaceOrderCommand.Command;
using Application.UseCases.PlaceOrderModule.Commands.CancelOrderCommand;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Api;

// OrderController.cs
[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IMediator mediator, ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order order)
    {
        try
        {
            var result = await _mediator.Send(new PlaceOrderCommand { Order = order });
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while placing order.");
            return StatusCode(500, "An error occurred while placing order.");
        }
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        try
        {
            var result = await _mediator.Send(new CancelOrderCommand { OrderId = orderId });
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while cancelling order.");
            return StatusCode(500, "An error occurred while cancelling order.");
        }
    }

}