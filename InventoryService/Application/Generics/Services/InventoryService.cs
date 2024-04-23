using Application.Generics.Interfaces;

namespace Application.Generics.Services;

public class InventoryService: IInventoryService
{
    public Task<int> CheckInventory(string order)
    {
        throw new NotImplementedException();
    }
}