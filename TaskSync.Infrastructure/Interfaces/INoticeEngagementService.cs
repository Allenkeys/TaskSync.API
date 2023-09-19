using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;

namespace TaskSync.Infrastructure.Interfaces;

public interface INoticeEngagementService
{
    Task ToggleRead(string userId, int noticeId);
    Task<Notification> GetNotification(string userId, int noticeId);
    Task<IEnumerable<Notification>> GetAllNotifications(string userId);
}
