using MediatR;

namespace Domain.Events;

public class OrderPlacedEvent : INotification
{
    public int OrderId { get; set; }
}