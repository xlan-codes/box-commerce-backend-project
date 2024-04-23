using Application.UseCases.VechicleAssemblyModule.Command;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers.Api;



// VehicleAssemblyController.cs
[ApiController]
[Route("api/assembly")]
public class VehicleAssemblyController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VehicleAssemblyController> _logger;

    public VehicleAssemblyController(IMediator mediator, ILogger<VehicleAssemblyController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost]
    public async Task<IActionResult> AssembleVehicle(VehicleAssembly vehicleAssembly)
    {
        try
        {
            var result = await _mediator.Send(new AssembleVehicleCommand { VehicleAssembly = vehicleAssembly });
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while assembling vehicle.");
            return StatusCode(500, "An error occurred while assembling vehicle.");
        }
    }
}