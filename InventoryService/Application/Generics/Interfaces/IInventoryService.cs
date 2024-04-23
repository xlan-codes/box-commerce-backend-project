using Domain.Entity;

namespace Application.Generics.Interfaces;

public interface IInventoryService
{
    Task<int> CheckInventory(String order);
}