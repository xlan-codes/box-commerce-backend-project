using Application.Contracts.Persistence;
using Application.Generics.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.InventoryModule.Queries;

// CheckInventoryQuery.cs
public class CheckInventoryQuery : IRequest<int>
{
    public string ComponentType { get; set; }
}

public class CheckInventoryQueryHandler : IRequestHandler<CheckInventoryQuery, int>
{
    private readonly ILogger<CheckInventoryQueryHandler> _logger;
    private readonly IInventoryService _inventoryService;

    public CheckInventoryQueryHandler(ILogger<CheckInventoryQueryHandler> logger, IInventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }

    public async Task<int> Handle(CheckInventoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Retrieve inventory count for the specified component type
            var inventoryCount = await _inventoryService.CheckInventory(request.ComponentType);
            return inventoryCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling CheckInventoryQuery.");
            throw; // Rethrow exception for further handling
        }
    }
}