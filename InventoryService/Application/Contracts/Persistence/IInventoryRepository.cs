namespace Application.Contracts.Persistence;

// IInventoryRepository.cs
public interface IInventoryRepository
{
    Task<int> GetInventoryCount(string componentType);
}