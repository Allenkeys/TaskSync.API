using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskSync.Application.Repository;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Notification> _noticeRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _noticeRepo = _unitOfWork.GetRepository<Notification>();
    }
    public async Task<SuccessResponse> CreateNotification(string userId, CreateNotificationRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new ArgumentException("User not found");
        var notification = _mapper.Map<Notification>(request);
        notification.UserId = userId;

        _noticeRepo.Create(notification);

        return new SuccessResponse { Success = true, Data = "Created" };
    }

    public Task<string> SendEmailNotification(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string> SendEmailNotification(IEnumerable<string> emails)
    {
        throw new NotImplementedException();
    }
}
