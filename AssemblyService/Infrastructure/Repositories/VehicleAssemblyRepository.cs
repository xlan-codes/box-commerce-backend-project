using Application.Contracts.Persistence;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class VehicleAssemblyRepository: IVehicleAssemblyRepository
{
    public Task<bool> SaveVehicleAssembly(VehicleAssembly vehicleAssembly)
    {
        throw new NotImplementedException();
    }
}