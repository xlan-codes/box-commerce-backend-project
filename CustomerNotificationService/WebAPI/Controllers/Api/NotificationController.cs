using Application.UseCases.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Api;

// NotificationController.cs
[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification(Notification notification)
    {
        var result = await _mediator.Send(new SendNotificationCommand { Notification = notification });
        return Ok(result);
    }
}