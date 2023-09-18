using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskSync.Application.Context;
using TaskSync.Application.Repository;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationService : INotificationService
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationService(IServiceProvider provider)
    {
        _serviceProvider = provider;
    }


    public async Task CreateDueDateNotification()
    {
        var ticketRepo = _serviceProvider.GetRequiredService<ApplicationDbContext>().Tickets;
        var noticeRepo = _serviceProvider.GetRequiredService<ApplicationDbContext>().Notifications;

        var dueTickets = ticketRepo.Where(t => t.DueDate.Equals(DateTime.Now.AddDays(-2))
            && t.Notifications.Any(x => x.NotificationTypeId != NotificationType.DueDate)).AsNoTracking();
        if (dueTickets == null) return;

        IList<Notification> notifications = null;

        foreach (var ticket in dueTickets)
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

        await noticeRepo.AddRangeAsync(notifications);
    }

    public Task<SuccessResponse> CreateNotification(string userId, CreateNotificationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task CreateStatusNotification()
    {
        var ticketRepo = _serviceProvider.GetRequiredService<ApplicationDbContext>().Tickets;
        var noticeRepo = _serviceProvider.GetRequiredService<ApplicationDbContext>().Notifications;

        var dueTickets = ticketRepo.Where(t => t.Status.Equals(Status.Completed)
           && t.Notifications.Any(x => x.NotificationTypeId != NotificationType.StatusUpdate)).AsNoTracking();
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

        await noticeRepo.AddRangeAsync(notifications);
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
