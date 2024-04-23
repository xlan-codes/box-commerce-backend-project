using Application.Contracts.Persistence;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using IMediator = MediatR.IMediator;

namespace Application.UseCases.VechicleAssemblyModule.Command;

// AssembleVehicleCommand.cs
public class AssembleVehicleCommand : IRequest<bool>
{
    public VehicleAssembly VehicleAssembly { get; set; }
}

public class AssembleVehicleCommandHandler : IRequestHandler<AssembleVehicleCommand, bool>
{
    private readonly ILogger<AssembleVehicleCommandHandler> _logger;
    private readonly IVehicleAssemblyRepository _vehicleAssemblyRepository;
    private readonly IMediator _mediator;

    public AssembleVehicleCommandHandler(ILogger<AssembleVehicleCommandHandler> logger, IVehicleAssemblyRepository vehicleAssemblyRepository, IMediator mediator)
    {
        _logger = logger;
        _vehicleAssemblyRepository = vehicleAssemblyRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(AssembleVehicleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vehicleAssembly = request.VehicleAssembly;

            // Logic to assemble vehicle
            // For simplicity, let's assume all components are available and assembly is successful

            // Update assembly status as completed
            vehicleAssembly.Status = AssemblyStatus.Completed;

            // Save the assembly
            await _vehicleAssemblyRepository.SaveVehicleAssembly(vehicleAssembly);

            // Publish event after vehicle is assembled
            await _mediator.Publish(new VehicleAssembledEvent { VehicleAssemblyId = vehicleAssembly.Id });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling AssembleVehicleCommand.");
            throw; // Rethrow exception for further handling
        }
    }
}
