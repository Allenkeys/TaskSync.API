using Microsoft.AspNetCore.Identity;
using TaskSync.Application.Repository;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.Interfaces;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationEngagementService : INoticeEngagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Notification> _noticeRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public NotificationEngagementService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _noticeRepo = _unitOfWork.GetRepository<Notification>();
    }

    public Task CreateNotification(string userId, CreateNotificationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Notification>> GetAllNotifications(string userId)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new ArgumentException("User not found");
        var notifications = _noticeRepo.FindBy(n => n.UserId.Equals(userId),
            trackChanges: false);
        if(notifications  == null) return Enumerable.Empty<Notification>();

        return notifications;
    }

    public async Task<Notification> GetNotification(string userId, int noticeId)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new ArgumentException("User not found");
        var notification = _noticeRepo.FindSingleBy(n => n.UserId.Equals(userId)
            && n.NotificationId.Equals(noticeId), trackChanges: false) ?? throw new ArgumentException("Notification not found");

        return notification;
    }

    public async Task ToggleRead(string userId, int noticeId)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new ArgumentException("User not found");
        var notification = _noticeRepo.FindSingleBy(n => n.UserId.Equals(userId) 
            && n.NotificationId.Equals(noticeId), trackChanges: true) ?? throw new ArgumentException("Notification not found");

        notification.IsRead = !notification.IsRead;
        _noticeRepo.Update(notification);
    }
}
