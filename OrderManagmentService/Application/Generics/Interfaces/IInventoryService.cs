using Domain.Entities;

namespace Application.Generics.Interfaces;

public interface IInventoryService
{
    Task<int> GetInventoryCount(string componentType);
}
