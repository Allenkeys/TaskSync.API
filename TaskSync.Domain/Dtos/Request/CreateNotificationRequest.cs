using TaskSync.Domain.Enums;

namespace TaskSync.Domain.Dtos.Request;

public class CreateNotificationRequest
{
    public NotificationType NotificationTypeId { get; set; }
    public bool IsRead { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
}
