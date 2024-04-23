using Application.Generics.Interfaces;
using Application.Schedulers;
using Domain.Entities;
using Domain.Events;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.PlaceOrderCommand.Command;

public class PlaceOrderCommand : IRequest<bool>
{
    public Order Order { get; set; }
}

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, bool>
{
    private readonly ILogger<PlaceOrderCommandHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IProductionScheduler _productionScheduler;
    private readonly IMediator _mediator;

    public PlaceOrderCommandHandler(ILogger<PlaceOrderCommandHandler> logger, IOrderRepository orderRepository, IInventoryService inventoryService, IProductionScheduler productionScheduler, IMediator mediator)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _inventoryService = inventoryService;
        _productionScheduler = productionScheduler;
        _mediator = mediator;
    }

    public async Task<bool> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = request.Order;

            // Check if all components in the order are available in inventory
            foreach (var orderItem in order.OrderItems)
            {
                var inventoryCount = await _inventoryService.GetInventoryCount(orderItem.ComponentType);
                if (inventoryCount < orderItem.Quantity)
                {
                    // If not enough inventory, schedule production for the component
                    await _productionScheduler.ScheduleProduction(orderItem.ComponentType, orderItem.Quantity - inventoryCount);
                }
            }

            // Update order status as in progress
            order.Status = OrderStatus.InProgress;

            // Save the order
            await _orderRepository.SaveOrder(order);

            // Publish event after order is placed
            await _mediator.Publish(new OrderPlacedEvent { OrderId = order.Id });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling PlaceOrderCommand.");
            throw; // Rethrow exception for further handling
        }
    }
}
