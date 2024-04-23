using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IVehicleAssemblyRepository
{
    Task<bool> SaveVehicleAssembly(VehicleAssembly vehicleAssembly);
}
