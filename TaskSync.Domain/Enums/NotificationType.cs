namespace TaskSync.Domain.Enums;

public enum NotificationType
{
    DueDate = 1,
    StatusUpdate,
}

public static class NotificationTypeExtension
{
    public static string? ToStringValue(this NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.DueDate:
                return "Due Date";
            case NotificationType.StatusUpdate:
                return "Status Update";
            default: return null;
        }
    }
}