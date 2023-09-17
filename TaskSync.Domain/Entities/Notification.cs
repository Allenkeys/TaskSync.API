using TaskSync.Domain.Enums;

namespace TaskSync.Domain.Entities;

public class Notification
{
    public int NotificationId { get; set; }
    public NotificationType NotificationTypeId { get; set; } 
    public bool IsRead { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public ApplicationUser User { get; set; }
}
