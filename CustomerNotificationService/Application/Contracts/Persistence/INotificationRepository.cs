using Domain.Entities;

namespace Application.Contracts.Persistence;

// INotificationRepository.cs
public interface INotificationRepository
{
    Task<bool> SendNotification(Notification notification);
}
