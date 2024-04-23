using Domain.Enums;

namespace Domain.Entities;

// Notification.cs
public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string Recipient { get; set; }
    public NotificationStatus Status { get; set; }
}