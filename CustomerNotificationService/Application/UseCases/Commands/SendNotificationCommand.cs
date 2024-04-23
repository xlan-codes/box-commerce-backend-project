using Application.Contracts.Persistence;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Commands;

public class SendNotificationCommand : IRequest<bool>
{
    public Notification Notification { get; set; }
}

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, bool>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<SendNotificationCommandHandler> _logger;

    public SendNotificationCommandHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var notification = request.Notification;

            // Logic to send notification
            // Update notification status accordingly

            // For simplicity, assume notification is always sent successfully
            notification.Status = NotificationStatus.Sent;

            // Save notification status
            await _notificationRepository.SendNotification(notification);
            _logger.LogInformation("Notification Sent Successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling SendNotificationCommand.");
            throw; // Rethrow exception for further handling
        }
    }
}