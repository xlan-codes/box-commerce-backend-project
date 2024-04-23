using MediatR;

namespace Domain.Events;

public class VehicleAssembledEvent : INotification
{
    public int VehicleAssemblyId { get; set; }
}