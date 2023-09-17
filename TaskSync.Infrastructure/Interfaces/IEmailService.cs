namespace TaskSync.Infrastructure.Interfaces;

public interface IEmailService
{
    Task<string> SendEmailAsync(string email);
    Task<string> SendBulkEmailAsync(IEnumerable<string> emails);
}
