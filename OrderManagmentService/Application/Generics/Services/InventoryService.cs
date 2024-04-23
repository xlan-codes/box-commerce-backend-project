using Application.Generics.Interfaces;

namespace Application.Generics.Services;

public class InventoryService: IInventoryService
{
    public Task<int> GetInventoryCount(string componentType)
    {
        throw new NotImplementedException();
    }
}