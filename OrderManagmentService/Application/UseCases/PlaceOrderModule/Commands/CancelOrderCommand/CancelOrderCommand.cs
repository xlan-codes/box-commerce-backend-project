using Infrastructure.Interfaces;
using MediatR;

namespace Application.UseCases.PlaceOrderModule.Commands.CancelOrderCommand;

// CancelOrderCommand.cs
public class CancelOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }
}

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        // Logic to cancel order
        // Update order status accordingly
        // Send events for further processing
        return true;
    }
}
