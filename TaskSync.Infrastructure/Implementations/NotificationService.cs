using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskSync.Application.Repository;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Notification> _noticeRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Ticket> _ticketRepo;
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

    public async Task CreateDueDateNotification()
    {
        var dueTickets = _ticketRepo.FindBy(t => t.DueDate.Equals(DateTime.Now.AddDays(-2)) 
            && t.Notifications.Any(x => x.NotificationTypeId != NotificationType.DueDate), 
            trackChanges: true);
        if (dueTickets == null) return;

        IList<Notification> notifications = null;

        foreach(var ticket in dueTickets)
        {
            notifications.Add(new Notification
            {
                TicketId = ticket.Id,
                Content = "Your ticket is due in 2 days",
                Created = DateTime.Now,
                NotificationTypeId = NotificationType.DueDate,
                UserId = ticket.Project.User.Id,
            });
        }

        _ticketRepo.BulkCreate(notifications);
    }

    public async Task CreateStatusNotification()
    {
        var dueTickets = _ticketRepo.FindBy(t => t.Status.Equals(Status.Completed)
           && t.Notifications.Any(x => x.NotificationTypeId != NotificationType.StatusUpdate),
           trackChanges: true);
        if (dueTickets == null) return;

        IList<Notification> notifications = null;

        foreach (var ticket in dueTickets)
        {
            notifications.Add(new Notification
            {
                TicketId = ticket.Id,
                Content = $"Congratulation your {ticket.Title} ticket is completed",
                Created = DateTime.Now,
                NotificationTypeId = NotificationType.StatusUpdate,
                UserId = ticket.Project.User.Id,
            });
        }

        _ticketRepo.BulkCreate(notifications);
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
