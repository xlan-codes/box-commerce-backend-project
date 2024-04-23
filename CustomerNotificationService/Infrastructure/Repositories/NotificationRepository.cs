using Application.Contracts.Persistence;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class NotificationRepository: INotificationRepository
{
    public Task<bool> SendNotification(Notification notification)
    {
        throw new NotImplementedException();
    }
}