using TaskSync.Domain.Dtos.Request;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Interfaces;

public interface INotificationService
{
    Task<string> SendEmailNotification(string email);
    Task<string> SendEmailNotification(IEnumerable<string> emails);
    Task CreateDueDateNotification();
    Task CreateStatusNotification();
    Task<SuccessResponse> CreateNotification(string userId, CreateNotificationRequest request);
}
